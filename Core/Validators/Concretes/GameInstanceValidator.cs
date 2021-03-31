using Core.Framework.Models;
using Core.Logic.Interfaces;
using Core.Models.Constants;
using Core.Models.Dtos;
using Core.Validators.Interfaces;
using System;
using System.Linq;

namespace Core.Validators.Concretes
{
    public class GameInstanceValidator : IGameInstanceValidator
    {
        public IGameLogic GameLogic { get; }
        public IUserLogic UserLogic { get; }
        public IRequestContext RequestContext { get; }
        public IGameInstanceLogic GameInstanceLogic { get; }

        public GameInstanceValidator(IGameLogic gameLogic, IUserLogic userLogic, IRequestContext requestContext, IGameInstanceLogic gameInstanceLogic)
        {
            GameLogic = gameLogic;
            UserLogic = userLogic;
            RequestContext = requestContext;
            GameInstanceLogic = gameInstanceLogic;
        }

        public IResponse<string> Validate(CreateGameInstanceDto dto)
        {
            var response = new Response<string>();

            var games = GameLogic.GetActiveGames();
            if (!games.Any(x => x.Id == dto.GameId))
                response.Errors.Add("That game is invalid.");
            if (string.IsNullOrWhiteSpace(dto.OpponentEmail) || dto.OpponentEmail == RequestContext.Email || UserLogic.Get(dto.OpponentEmail) == null)
                response.Errors.Add("That email address is invalid.");
            return response;
        }

        public IResponse<string> Validate(PlayGameInstanceDto dto)
        {
            var response = new Response<string>();

            var gameInstance = GameInstanceLogic.Get(dto.Id);
            if (gameInstance == null || !gameInstance.Users.Any(x => x.Role == GameInstanceRoles.Player.ToString() && x.UserId == RequestContext.UserId))
                response.Errors.Add("That game is invalid.");
            if (string.IsNullOrWhiteSpace(dto.UserInput))
                response.Errors.Add("User input is invalid.");
            return response;
        }

        public IResponse<string> Validate(ChatMessageDto dto)
        {
            var response = new Response<string>();
            if (dto.Id == Guid.Empty)
                response.Errors.Add("invalid game instance");
            if (string.IsNullOrWhiteSpace(dto.Text) || dto.Text.Length > 500)
                response.Errors.Add("invalid message text");

            var gameInstance = GameInstanceLogic.Get(dto.Id);
            if (gameInstance == null || !gameInstance.Users.Any(x => x.UserId == RequestContext.UserId))
                response.Errors.Add("That game is invalid.");
            return response;
        }
    }
}
