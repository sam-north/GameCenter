using Core.DataAccess;
using Core.Logic.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Logic.Concretes
{
    public class GameInstanceLogic : IGameInstanceLogic
    {
        private ModelContext ModelContext { get; }

        public GameInstanceLogic(ModelContext modelContext)
        {
            ModelContext = modelContext;
        }

        public ICollection<GameInstance> Get()
        {
            var results = new List<GameInstance>();

            results = ModelContext.GameInstances.Where(x => !x.IsDeleted).ToList();

            return results;
        }

        public GameInstance Get(Guid id)
        {
            var entity = ModelContext.GameInstances.Include(x => x.Users).Single(x => x.Id == id);
            entity.State = ModelContext.GameInstanceStates.Where(x => x.GameInstanceId == id).OrderByDescending(x => x.DateCreated).SingleOrDefault();
            return entity;
        }

        public GameInstance Save(GameInstance modelToSave)
        {
            var entity = ModelContext.GameInstances.SingleOrDefault(x => x.Id == modelToSave.Id);

            if (entity == null)
            {
                entity = new GameInstance();
                entity.DateCreated = DateTimeOffset.Now;
                entity.GameId = modelToSave.GameId;

                ModelContext.GameInstances.Add(entity);
                ModelContext.SaveChanges();
            }

            entity.State.GameInstanceId = entity.Id;

            SaveState(entity.State);
            SaveUsers(entity.Id, entity.Users);

            ModelContext.SaveChanges();
            return entity;
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

        private void SaveState(GameInstanceState modelToSave)
        {
            var entity = new GameInstanceState();
            entity.DateCreated = DateTimeOffset.Now;
            entity.DataAsJson = modelToSave.DataAsJson;
            entity.GameInstanceId = modelToSave.GameInstanceId;

            ModelContext.GameInstanceStates.Add(entity);
        }

        public void Delete(Guid id)
        {
            var entity = Get(id);

            entity.IsDeleted = true;

            ModelContext.SaveChanges();
        }
    }
}
