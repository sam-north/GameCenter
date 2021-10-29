using Core.Framework.Models;
using Core.Framework.Serializers;
using Core.Logic.Interfaces;
using Core.Mappers.Interfaces;
using Core.Models;
using Core.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Core.Logic.Concretes
{
    public abstract class BaseGameLogic<T> : IGameStrategy where T : class, IGameState
    {
        protected T _gameState;
        protected ICollection<GameInstanceUser> _gameInstanceUsers;
        protected IGameInstanceMapper GameInstanceMapper { get; }
        protected IResponse<string> _response { get; set; } = new Response<string>();
        public BaseGameLogic(IGameInstanceMapper gameInstanceMapper)
        {
            GameInstanceMapper = gameInstanceMapper;
        }

        public string GetDefaultGameState(GameInstance gameInstance)
        {
            _gameInstanceUsers = gameInstance.Users;
            SetupGame();
            return CreateGameStateAsString();
        }

        public IResponse<string> LoadAndPlayAndReturnGameStateAsString(GameInstance gameInstanceWithPreviousState, int userId, string userInput)
        {
            _gameInstanceUsers = gameInstanceWithPreviousState.Users;

            LoadGame(gameInstanceWithPreviousState);
            Play(userId, userInput);
            _response.Data = CreateGameStateAsString();
            return _response;
        }

        private void Play(int userId, string userInput)
        {
            CheckGameState(userId, userInput);
        }

        private void LoadGame(GameInstance gameInstanceWithPreviousState)
        {
            if (gameInstanceWithPreviousState != null)
                _gameState = JsonSerializer.Deserialize<T>(gameInstanceWithPreviousState.State.DataAsJson);
        }

        private string CreateGameStateAsString()
        {
            return JsonSerializer.Serialize(_gameState);
        }

        protected bool UserIsInvalidUser(int userId)
        {
            return !_gameInstanceUsers.Select(x => x.UserId).Any(x => x == userId);
        }

        protected abstract bool CheckForEndOfGame();
        protected abstract void CheckGameState(int userId, string userInput);
        protected abstract void SetupGame();
        protected abstract void SetResult();
    }
}
