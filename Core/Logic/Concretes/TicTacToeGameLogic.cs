using Core.Logic.Interfaces;
using Core.Mappers.Interfaces;
using Core.Models;
using Core.Models.Constants;
using Core.Models.Games;
using System.Collections.Generic;
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
                GameIsPlayable = true,
                HasGameBeenSetup = true,
                Board = new string[9],
                IsPlayer1Turn = true,
                Player1 = CreatePlayer(playerUsers.ElementAt(0)),
                Player2 = CreatePlayer(playerUsers.ElementAt(1)),
            };
            _response.Messages.Add("Welcome to Tic-Tac-Toe!");
        }

        private void Turn(int userId, string input)
        {
            _gameState.GameIsPlayable = IsGamePlayable();
            if (!_gameState.GameIsPlayable) return;

            if (userId == 0 || UserIsInvalidUser(userId))
            {
                _response.Errors.Add("Invalid user");
                return;
            }

            var currentPlayer = GetCurrentPlayer();
            var opponentPlayer = GetOpponentPlayer();
            if (userId != currentPlayer.User.UserId)
            {
                _response.Errors.Add("It's not your turn!");
                return;
            }

            var playerPossibleSpots = GetPlayerPossibleSpotsToMove();
            if (!(short.TryParse(input, out var indexToMove) && playerPossibleSpots.Contains(indexToMove)))
            {
                _response.Errors.Add($"Invalid move.  That is not an available space to play.");
                return;
            }

            Move(indexToMove);
        }

        private void Move(short index)
        {
            _gameState.Board[index] = _gameState.IsPlayer1Turn ? "X" : "O";
            _gameState.IsPlayer1Turn = !_gameState.IsPlayer1Turn;
        }

        protected override void SetResult()
        {
            var winner = FindWinner();
            if (winner == null)
            {
                _gameState.Result = Strings.GameInstanceResults.Tie;
                _response.Messages.Add("It's a cat! Nobody wins!");
                return;
            }

            var winningUser = winner == "X" ? _gameState.Player1 : _gameState.Player2;
            _gameState.Result = winningUser.User.UserId.ToString();
            _response.Messages.Add(winningUser.User?.UserEmail + " wins!");
        }

        protected override bool IsGamePlayable()
        { 
            return !(_gameState.Board.All(x => !string.IsNullOrWhiteSpace(x)) || !string.IsNullOrWhiteSpace(FindWinner()));
        }

        private string FindWinner()
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

            for (int i = 0; i < lines.GetLength(0); i++)
            {
                int a = lines[i, 0];
                int b = lines[i, 1];
                int c = lines[i, 2];
                if (
                    !string.IsNullOrWhiteSpace(_gameState.Board[a]) &
                   (_gameState.Board[a] == _gameState.Board[b]) &
                   (_gameState.Board[a] == _gameState.Board[c])
                    )
                {
                    return _gameState.Board[a];
                };
            }
            return null;
        }

        protected override void CheckGameState(int userId, string input)
        {
            if (_gameState.GameIsPlayable)
            {
                Turn(userId, input);
                _gameState.GameIsPlayable = IsGamePlayable();
                if (!_gameState.GameIsPlayable)
                    SetResult();
            }
            else SetResult();
        }

        private TicTacToePlayer CreatePlayer(GameInstanceUser gameInstanceUser)
        {
            var player = new TicTacToePlayer();
            player.User = GameInstanceMapper.Map(gameInstanceUser);
            return player;
        }

        private TicTacToePlayer GetOpponentPlayer()
        {
            return (_gameState.IsPlayer1Turn) ? _gameState.Player2 : _gameState.Player1;
        }

        private TicTacToePlayer GetCurrentPlayer()
        {
            return (_gameState.IsPlayer1Turn) ? _gameState.Player1 : _gameState.Player2;
        }

        private List<int> GetPlayerPossibleSpotsToMove()
        {
            var result = new List<int>();
            for (int i = 0; i < _gameState.Board.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(_gameState.Board[i]))
                    result.Add(i);
            }
            return result;
        }

    }
}
