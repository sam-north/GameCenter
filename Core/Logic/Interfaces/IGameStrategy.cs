using Core.Framework.Models;
using Core.Models;

namespace Core.Logic.Interfaces
{
    public interface IGameStrategy
    {
        string GetDefaultGameState(GameInstance gameInstance);
        Response<string> LoadAndPlayAndReturnGameStateAsString(GameInstance previousGameInstance, int userId, string userInput);
    }
}
