using Core.Logic.Interfaces;
using Core.Mappers.Interfaces;
using Core.Models;
using Core.Models.Constants;
using Core.Models.Games;
using System.Collections.Generic;
using System.Linq;

namespace Core.Logic.Concretes
{
    public class MancalaGameLogic : BaseGameLogic<MancalaGameState>, IMancalaGameLogic
    {
        public MancalaGameLogic(IGameInstanceMapper gameInstanceMapper)
            : base(gameInstanceMapper) { }

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

        protected override void SetResult()
        {
            var winner = (_gameState.Player1.Board[6] > _gameState.Player2.Board[6]) ? _gameState.Player1 : (_gameState.Player2.Board[6] > _gameState.Player1.Board[6]) ? _gameState.Player2 : null;

            if (winner == null)
            {
                _gameState.Result = Strings.GameInstanceResults.Tie;
                _response.Messages.Add("It's a tie! Nobody wins!");
                return;
            }

            _gameState.Result = winner.User.UserId.ToString();
            _response.Messages.Add(winner.User?.UserEmail + " wins!");
            _response.Messages.Add($"{_gameState.Player1.User?.UserEmail} had {_gameState.Player1.Board[6]} and {_gameState.Player2.User?.UserEmail} had {_gameState.Player2.Board[6]}");
        }

        protected override bool IsGamePlayable()
        {
            var player1PossibleMoves = GetPlayerPossibleSpotsToMove(_gameState.Player1);
            if (!player1PossibleMoves.Any())
            {
                for (int i = 0; i < 6; i++)
                {
                    _gameState.Player2.Board[6] += _gameState.Player2.Board[i];
                    _gameState.Player2.Board[i] = 0;
                }
                return false;
            }

            var player2PossibleMoves = GetPlayerPossibleSpotsToMove(_gameState.Player2);
            if (!player2PossibleMoves.Any())
            {

                for (int i = 0; i < 6; i++)
                {
                    _gameState.Player1.Board[6] += _gameState.Player1.Board[i];
                    _gameState.Player1.Board[i] = 0;
                }
                return false;
            }

            return true;
        }

        protected override void SetupGame()
        {
            var playerUsers = _gameInstanceUsers.Where(x => x.Role == GameInstanceRoles.Player.ToString());
            _gameState = new MancalaGameState
            {
                GameIsPlayable = true,
                IsPlayer1Turn = true,
                Player1 = CreatePlayer(playerUsers.ElementAt(0)),
                Player2 = CreatePlayer(playerUsers.ElementAt(1)),
                HasGameBeenSetup = true
            };
            _response.Messages.Add("Welcome to Mancala!");
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

            var playerPossibleSpots = GetPlayerPossibleSpotsToMove(currentPlayer);
            if (!(short.TryParse(input, out var indexToMove) && playerPossibleSpots.Contains(indexToMove)))
            {
                _response.Errors.Add($"Invalid move. Choose from: {string.Join(",", playerPossibleSpots)}");
                return;
            }

            Move(indexToMove - 1, currentPlayer, opponentPlayer);
        }

        private void Move(int indexToMove, MancalaPlayer currentPlayer, MancalaPlayer opponentPlayer)
        {
            var currentMarblesToMove = currentPlayer.Board[indexToMove];
            currentPlayer.Board[indexToMove] = 0;
            var currentTargetIndex = indexToMove + 1;
            var targetCurrentPlayerBoard = true;
            while (currentMarblesToMove > 0)
            {
                if (currentTargetIndex == 6)
                {
                    if (targetCurrentPlayerBoard)
                    {
                        currentPlayer.Board[currentTargetIndex] += 1;
                        currentMarblesToMove -= 1;
                        if (currentMarblesToMove == 0)
                            return;
                    }

                    targetCurrentPlayerBoard = !targetCurrentPlayerBoard;
                    currentTargetIndex = 0;
                }
                else
                {
                    if (targetCurrentPlayerBoard)
                    {
                        var opponentIndex = 5 - currentTargetIndex;
                        if (currentPlayer.Board[currentTargetIndex] == 0 && currentMarblesToMove == 1 && opponentPlayer.Board[opponentIndex] > 0)
                        {
                            currentPlayer.Board[6] += currentMarblesToMove;
                            currentMarblesToMove -= 1;
                            currentPlayer.Board[6] += opponentPlayer.Board[opponentIndex];
                            opponentPlayer.Board[opponentIndex] = 0;
                        }
                        else
                        {
                            MoveStandardSpot(currentPlayer, ref currentMarblesToMove, ref currentTargetIndex);
                        }
                    }
                    else
                        MoveStandardSpot(opponentPlayer, ref currentMarblesToMove, ref currentTargetIndex);
                }
            }

            _gameState.IsPlayer1Turn = !_gameState.IsPlayer1Turn;
        }

        private void MoveStandardSpot(MancalaPlayer player, ref int currentMarblesToMove, ref int currentTargetIndex)
        {
            player.Board[currentTargetIndex] += 1;
            currentMarblesToMove -= 1;
            currentTargetIndex++;
        }

        private List<int> GetPlayerPossibleSpotsToMove(MancalaPlayer currentPlayer)
        {
            var result = new List<int>();
            for (int i = 0; i < currentPlayer.Board.Length - 1; i++)
            {
                if (currentPlayer.Board[i] > 0)
                    result.Add(i + 1);
            }
            return result;
        }

        private MancalaPlayer CreatePlayer(GameInstanceUser gameInstanceUser)
        {
            var player = new MancalaPlayer();
            player.Board = new int[7] { 4, 4, 4, 4, 4, 4, 0 };
            player.User = GameInstanceMapper.Map(gameInstanceUser);
            return player;
        }

        private MancalaPlayer GetOpponentPlayer()
        {
            return (_gameState.IsPlayer1Turn) ? _gameState.Player2 : _gameState.Player1;
        }

        private MancalaPlayer GetCurrentPlayer()
        {
            return (_gameState.IsPlayer1Turn) ? _gameState.Player1 : _gameState.Player2;
        }
    }
}