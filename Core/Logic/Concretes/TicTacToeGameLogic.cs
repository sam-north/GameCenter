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
            _gameInstanceUsers = gameInstanceWithPreviousState.Users;

            LoadGame(gameInstanceWithPreviousState);
            Play(userId, userInput);
            var response = new Response<string>();
 
            return response;
        }

        private void Play(int userId, string userInput)
        {
            CheckGameState(userId, userInput);
        }

        private void LoadGame(GameInstance gameInstanceWithPreviousState)
        {
            if (gameInstanceWithPreviousState != null)
                _gameState = JsonSerializer.Deserialize<TicTacToeGameState>(gameInstanceWithPreviousState.State.DataAsJson);
        }

        private string CreateGameStateAsString()
        {
            string jsonString = JsonSerializer.Serialize(_gameState);
            return jsonString;
        }
        
        private void CheckGameState(int userId, string input)
        {
            if (_gameState.Winner == null) Turn(userId, input);
            else AnnounceWinner();
        }

        private void AnnounceWinner()
        {
            // TODO write this
            throw new NotImplementedException();
        }

        private void Turn(int userId, string input)
        {
            _gameState.Winner = CheckForEndOfGame();
        }

        private string CheckForEndOfGame()
        {
            int[,] lines = {
                {0, 1, 2},
                {3, 4, 5},
                {6, 7, 8},
                {0, 3, 6},
                {1, 4, 7},
                {2, 5, 8},
                {0, 4, 8},
                {2, 4, 8}
            };

            for (int i = 0; i < lines.Length; i++)
            {
                int a = lines[i, 0];
                int b = lines[i, 1];
                int c = lines[i, 2];
                if (
                    _gameState.Board[a] != null &
                   (_gameState.Board[a] == _gameState.Board[b]) &
                   (_gameState.Board[a] == _gameState.Board[c])
                    )
                {
                    return _gameState.Board[a];
                };

            }
            return null;
        }
    }
}
