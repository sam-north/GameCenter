using Core.Framework.Models;
using Core.Logic.Interfaces;
using Core.Models;
using Core.Models.Constants;
using Core.Models.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Core.Logic.Concretes
{
    public class TicTacToeGameLogic : ITicTacToeGameLogic
    {

        private TicTacToeGameState _gameState;
        private ICollection<GameInstanceUser> _gameInstanceUsers;
        private IResponse<string> _response { get; set; } = new Response<string>();
        public string GetDefaultGameState(GameInstance gameInstance)
        {
            _gameInstanceUsers = gameInstance.Users;
            SetupGame();
            return CreateGameStateAsString();

        }

        private string CreateGameStateAsString()
        {
            string jsonString = JsonSerializer.Serialize(_gameState);
            return jsonString;
        }

        private void SetupGame()
        {
            var playerUsers = _gameInstanceUsers.Where(x => x.Role == GameInstanceRoles.Player.ToString());
            _gameState = new TicTacToeGameState
            {
                HasGameBeenSetup = true,
                Board = new string[9],
                xIsNext = true,
                Player1 = CreatePlayer(playerUsers.ElementAt(0)),
                Player2 = CreatePlayer(playerUsers.ElementAt(1)),
            };
            _response.Messages.Add("Welcome to Tic-Tac-Toe!");
        }

        private string CreatePlayer(GameInstanceUser gameInstanceUser)
        {
            return gameInstanceUser.User.Email;
        }

        public IResponse<string> LoadAndPlayAndReturnGameStateAsString(GameInstance gameInstanceWithPreviousState, int userId, string userInput)
        {
            var response = new Response<string>();

            return response;
        }
    }
}
