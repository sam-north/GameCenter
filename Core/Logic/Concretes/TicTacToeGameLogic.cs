using Core.Framework.Models;
using Core.Logic.Interfaces;
using Core.Models;

namespace Core.Logic.Concretes
{
    public class TicTacToeGameLogic : ITicTacToeGameLogic
    {
        public string GetDefaultGameState(GameInstance gameInstance)
        {
            return "DEFAULT TICTACTOE GAME STATE";
        }

        public IResponse<string> LoadAndPlayAndReturnGameStateAsString(GameInstance gameInstanceWithPreviousState, int userId, string userInput)
        {
            var response = new Response<string>();

            return response;
        }
    }
}
