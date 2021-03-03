using Core.Framework.Models;
using Core.Logic.Interfaces;
using Core.Models.Dtos;
using Core.Validators.Interfaces;
using System.Linq;

namespace Core.Validators.Concretes
{
    public class GameInstanceValidator : IGameInstanceValidator
    {
        public IGameLogic GameLogic { get; }
        public IUserLogic UserLogic { get; }

        public GameInstanceValidator(IGameLogic gameLogic, IUserLogic userLogic)
        {
            GameLogic = gameLogic;
            UserLogic = userLogic;
        }

        public Response<string> Validate(CreateGameInstanceDto dto)
        {
            var response = new Response<string>();

            var games = GameLogic.GetActiveGames();
            if (!games.Any(x => x.Id == dto.GameId))
                response.Errors.Add("That game is invalid.");
            if (string.IsNullOrWhiteSpace(dto.OpponentEmail) || UserLogic.Get(dto.OpponentEmail) == null)
                response.Errors.Add("That email address is invalid.");
            return response;
        }
    }
}
