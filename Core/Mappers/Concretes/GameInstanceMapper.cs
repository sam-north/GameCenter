using Core.Framework.Models;
using Core.Mappers.Interfaces;
using Core.Models;
using Core.Models.Dtos;
using Core.Models.Views;
using System.Collections.Generic;
using System.Linq;

namespace Core.Mappers.Concretes
{
    public class GameInstanceMapper : IGameInstanceMapper
    {
        IRequestContext RequestContext { get; }

        public GameInstanceMapper(IRequestContext requestContext)
        {
            RequestContext = requestContext;
        }

        public GameInstanceDto Map(GameInstance source)
        {
            var target = new GameInstanceDto();
            target.Id = source.Id;
            target.GameDisplayName = source.Game.DisplayName;
            target.DateCreated = source.DateCreated;
            target.GameId = source.GameId;
            target.State = Map(source.State);
            target.Users = Map(source.Users);
            return target;
        }

        public GameInstance Map(GameInstanceDto source)
        {
            var target = new GameInstance();
            target.Id = source.Id;
            target.GameId = source.GameId;
            target.State = Map(source.State);
            target.Users = Map(source.Users);
            return target;
        }

        public GameInstanceUserDto Map(GameInstanceUser gameInstanceUser)
        {
            var target = new GameInstanceUserDto();
            target.UserId = gameInstanceUser.UserId;
            target.Role = gameInstanceUser.Role;
            target.UserEmail = gameInstanceUser.User?.Email;
            return target;
        }

        public ICollection<UserGameInstanceStatelessDto> Map(ICollection<GameInstance> gameInstances)
        {
            var results = new List<UserGameInstanceStatelessDto>();
            foreach (var gameInstance in gameInstances)
                results.Add(MapUserGameInstanceListDto(gameInstance));
            return results;
        }

        public UserGameInstanceStatelessDto MapUserGameInstanceListDto(GameInstance source)
        {
            var target = new UserGameInstanceStatelessDto();
            target.DateCreated = source.DateCreated;
            target.GameDisplayName = source.Game.DisplayName;
            target.GameId = source.GameId;
            target.Id = source.Id;
            target.Users = Map(source.Users);
            target.Users = target.Users.Where(x => x.UserId != RequestContext.UserId).ToList();
            return target;
        }

        public ICollection<GameInstanceUserMessageDto> Map(ICollection<GameInstanceUserMessageViewResult> messages)
        {
            var targets = new List<GameInstanceUserMessageDto>();
            foreach (var message in messages)
                targets.Add(Map(message));
            return targets;
        }

        public GameInstanceUserMessageDto Map(GameInstanceUserMessage source)
        {
            var target = new GameInstanceUserMessageDto();
            target.DateCreated = source.DateCreated;
            target.Id = source.Id;
            target.Text = source.Text;
            target.UserEmail = source.User?.Email;
            target.UserId = source.UserId;
            return target;
        }

        private GameInstanceUserMessageDto Map(GameInstanceUserMessageViewResult source)
        {
            var target = new GameInstanceUserMessageDto();
            target.DateCreated = source.DateCreated;
            target.Id = source.Id;
            target.Text = source.Text;
            target.UserEmail = source.UserEmail;
            target.UserId = source.UserId;
            return target;
        }

        private GameInstanceStateDto Map(GameInstanceState gameInstanceState)
        {
            var target = new GameInstanceStateDto();
            target.DataAsJson = gameInstanceState.DataAsJson;
            target.DateCreated = gameInstanceState.DateCreated;
            return target;
        }

        private ICollection<GameInstanceUser> Map(ICollection<GameInstanceUserDto> gameInstanceUserDtos)
        {
            var targets = new List<GameInstanceUser>();
            foreach (var gameInstanceUserDto in gameInstanceUserDtos)
                targets.Add(Map(gameInstanceUserDto));
            return targets;
        }

        private GameInstanceUser Map(GameInstanceUserDto gameInstanceUserDto)
        {
            var target = new GameInstanceUser();
            target.Role = gameInstanceUserDto.Role;
            target.UserId = gameInstanceUserDto.UserId;
            return target;
        }

        private GameInstanceState Map(GameInstanceStateDto gameInstanceStateDto)
        {
            var target = new GameInstanceState();
            target.DataAsJson = gameInstanceStateDto.DataAsJson;
            return target;
        }

        private ICollection<GameInstanceUserDto> Map(ICollection<GameInstanceUser> gameInstanceUsers)
        {
            var targets = new List<GameInstanceUserDto>();
            foreach (var gameInstanceUser in gameInstanceUsers)
                targets.Add(Map(gameInstanceUser));
            return targets;
        }
    }
}
