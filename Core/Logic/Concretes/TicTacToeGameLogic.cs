using Core.Logic.Interfaces;
using Core.Mappers.Interfaces;
using Core.Models;
using Core.Models.Constants;
using Core.Models.Games;
using System;
using System.Linq;

namespace Core.Logic.Concretes
{
    public class TicTacToeGameLogic : BaseGameLogic<TicTacToeGameState>, ITicTacToeGameLogic
    {
        public TicTacToeGameLogic(IGameInstanceMapper gameInstanceMapper)
            : base(gameInstanceMapper) { }

        protected override void SetupGame()
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

        protected override void SetResult()
        {
            throw new NotImplementedException();
        }

        protected override bool CheckForEndOfGame()
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
                    return true; // _gameState.Board[a] ;
                };

            }
            return false;
        }

        protected override void CheckGameState(int userId, string input)
        {
            if (_gameState.Winner == null) Turn(userId, input);
            else AnnounceWinner();
        }

        private string CreatePlayer(GameInstanceUser gameInstanceUser)
        {
            return gameInstanceUser.User.Email;
        }

        private void AnnounceWinner()
        {
            // TODO write this
            throw new NotImplementedException();
        }

        private void Turn(int userId, string input)
        {
            throw new NotImplementedException();
        }
    }
}
