using Core.DataAccess;
using Core.Framework.Mappers;
using Core.Framework.Models;
using Core.Logic.Interfaces;
using Core.Models;
using Core.Models.Constants;
using Core.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Logic.Concretes
{
    public class GameInstanceLogic : IGameInstanceLogic
    {
        private ModelContext ModelContext { get; }
        public IUserLogic UserLogic { get; }
        public IRequestContext RequestContext { get; }
        public IGameStrategyProvider GameStrategyProvider { get; }

        public GameInstanceLogic(ModelContext modelContext, IUserLogic userLogic, IRequestContext requestContext, IGameStrategyProvider gameStrategyProvider)
        {
            ModelContext = modelContext;
            UserLogic = userLogic;
            RequestContext = requestContext;
            GameStrategyProvider = gameStrategyProvider;
        }

        public ICollection<GameInstance> Get()
        {
            var results = new List<GameInstance>();

            results = ModelContext.GameInstances.Where(x => !x.IsDeleted).ToList();

            return results;
        }

        public ICollection<GameInstance> GetCurrentUserGameInstancesWithoutGameStates()
        {
            var results = new List<GameInstance>();

            results = ModelContext.GameInstances
                                                .Include(x => x.Game)
                                                .Include(x => x.Users)
                                                    .ThenInclude(x => x.User)
                                                .Where(x => !x.IsDeleted
                                                            && x.Users.Select(x => x.UserId).Contains(RequestContext.UserId))
                                                .OrderBy(x => x.DateCreated).ToList();
            return results;
        }

        public GameInstance Get(Guid id)
        {
            var entity = ModelContext.GameInstances
                                                    .Include(x => x.Game)
                                                    .Include(x => x.Users)
                                                        .ThenInclude(x => x.User)
                                                    .SingleOrDefault(x => x.Id == id);
            if (entity != null)
                entity.State = ModelContext.GameInstanceStates.Where(x => x.GameInstanceId == id).OrderByDescending(x => x.DateCreated).SingleOrDefault();
            return entity;
        }

        public GameInstance Save(GameInstance modelToSave)
        {
            var entity = Get(modelToSave.Id); //ModelContext.GameInstances.SingleOrDefault(x => x.Id == modelToSave.Id);

            if (entity == null)
            {
                entity = new GameInstance();
                entity.DateCreated = DateTimeOffset.Now;
                entity.GameId = modelToSave.GameId;

                ModelContext.GameInstances.Add(entity);
                ModelContext.SaveChanges();
            }

            SaveState(entity.Id, modelToSave.State);
            SaveUsers(entity.Id, modelToSave.Users);

            ModelContext.SaveChanges();
            return entity;
        }

        private void SaveState(Guid gameInstanceId, GameInstanceState state)
        {
            var entity = new GameInstanceState();

            entity.DateCreated = DateTimeOffset.Now;
            entity.DataAsJson = state.DataAsJson;
            entity.GameInstanceId = gameInstanceId;

            ModelContext.GameInstanceStates.Add(entity);
        }

        public void Delete(Guid id)
        {
            var entity = Get(id);

            entity.IsDeleted = true;

            ModelContext.SaveChanges();
        }

        private void SaveUsers(Guid gameInstanceId, ICollection<GameInstanceUser> modelUsersToSave)
        {
            var existingGameInstanceUsers = ModelContext.GameInstanceUsers.Where(x => x.GameInstanceId == gameInstanceId).ToList();
            var existingUserIdsOnly = existingGameInstanceUsers.Select(x => x.UserId);
            var modelUsersToSaveUserIdsOnly = modelUsersToSave.Select(x => x.UserId);
            var userIdsToBeAdded = modelUsersToSaveUserIdsOnly.Except(existingUserIdsOnly);
            var userIdsToBeDeleted = existingUserIdsOnly.Except(modelUsersToSaveUserIdsOnly);
            var userIdsToPotentiallyUpdate = modelUsersToSaveUserIdsOnly.Intersect(existingUserIdsOnly);

            AddUsers(gameInstanceId, modelUsersToSave, userIdsToBeAdded);
            RemoveUsers(modelUsersToSave, existingGameInstanceUsers, userIdsToBeDeleted);
            PotentiallyUpdateUsers(modelUsersToSave, existingGameInstanceUsers, userIdsToPotentiallyUpdate);
        }

        private void PotentiallyUpdateUsers(ICollection<GameInstanceUser> modelUsersToSave, ICollection<GameInstanceUser> existingGameInstanceUsers, IEnumerable<int> userIdsToPotentiallyModify)
        {
            foreach (var userId in userIdsToPotentiallyModify)
            {
                var modelToSave = modelUsersToSave.Single(x => x.UserId == userId);
                var entity = existingGameInstanceUsers.Single(x => x.UserId == modelToSave.UserId);
                if (entity.Role != modelToSave.Role)
                    entity.Role = modelToSave.Role;
            }
        }

        private void RemoveUsers(ICollection<GameInstanceUser> modelUsersToSave, ICollection<GameInstanceUser> existingGameInstanceUsers, IEnumerable<int> userIdsToBeDeleted)
        {
            foreach (var userId in userIdsToBeDeleted)
            {
                var modelToSave = modelUsersToSave.Single(x => x.UserId == userId);
                var entity = existingGameInstanceUsers.Single(x => x.UserId == modelToSave.UserId && x.Role == modelToSave.Role);
                ModelContext.GameInstanceUsers.Remove(entity);
            }
        }

        private void AddUsers(Guid gameInstanceId, ICollection<GameInstanceUser> modelUsersToSave, IEnumerable<int> userIdsToBeAdded)
        {
            foreach (var userId in userIdsToBeAdded)
            {
                var modelToSave = modelUsersToSave.Single(x => x.UserId == userId);
                var entity = new GameInstanceUser();
                entity.GameInstanceId = gameInstanceId;
                entity.Role = modelToSave.Role;
                entity.UserId = modelToSave.UserId;

                ModelContext.GameInstanceUsers.Add(entity);
            }
        }

        private GameInstanceState CreateGameStateObject(string stateDataAsJson)
        {
            var entity = new GameInstanceState();

            entity.DataAsJson = stateDataAsJson;
            return entity;
        }

        public IResponse<GameInstance> New(CreateGameInstanceDto dto)
        {
            var response = new Response<GameInstance>();
            var gameInstance = new GameInstance();
            gameInstance.GameId = dto.GameId;

            //add state
            var gameStrategy = GameStrategyProvider.Provide(gameInstance.GameId);
            gameInstance.State = CreateGameStateObject(gameStrategy.GetDefaultGameState(gameInstance));

            //add users
            gameInstance.Users.Add(new GameInstanceUser
            {
                UserId = RequestContext.UserId,
                Role = GameInstanceRoles.Player.ToString()
            });
            var opposingUser = UserLogic.Get(dto.OpponentEmail);
            gameInstance.Users.Add(new GameInstanceUser
            {
                UserId = opposingUser.Id,
                Role = GameInstanceRoles.Player.ToString()
            });

            response.Data = Save(gameInstance);
            return response;
        }

        public IResponse<GameInstance> Play(PlayGameInstanceDto dto)
        {
            var gameInstance = Get(dto.Id);
            var gameStrategy = GameStrategyProvider.Provide(gameInstance.GameId);

            var newGameStateAsJsonStringResponse = gameStrategy.LoadAndPlayAndReturnGameStateAsString(gameInstance, RequestContext.UserId, dto.UserInput);
            var response = ResponseMapper.MapMetadata<GameInstance>(newGameStateAsJsonStringResponse);

            if (!response.IsValid || response.Messages.Any(x => x.Contains("wins!")))
                return response;

            var newGameState = CreateGameStateObject(newGameStateAsJsonStringResponse.Data);
            gameInstance.State = newGameState;

            response.Data = Save(gameInstance);
            return response;
        }
    }
}
