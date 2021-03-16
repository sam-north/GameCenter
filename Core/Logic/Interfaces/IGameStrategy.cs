using Core.Framework.Models;
using Core.Models;

namespace Core.Logic.Interfaces
{
    public interface IGameStrategy
    {
        string GetDefaultGameState(GameInstance gameInstance);
        IResponse<string> LoadAndPlayAndReturnGameStateAsString(GameInstance gameInstanceWithPreviousState, int userId, string userInput);
    }
}
