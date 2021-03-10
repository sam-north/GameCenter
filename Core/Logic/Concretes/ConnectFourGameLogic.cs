using Core.Framework.Models;
using Core.Logic.Interfaces;
using Core.Models;

namespace Core.Logic.Concretes
{
    public class ConnectFourGameLogic : IConnectFourGameLogic
    {
        public string GetDefaultGameState(GameInstance gameInstance)
        {
            return "DEFAULT CONNECT FOUR GAME STATE";
        }

        public Response<string> LoadAndPlayAndReturnGameStateAsString(GameInstance previousGameInstance, int userId, string userInput)
        {
            var response = new Response<string>();

            return response;
        }
    }
}
