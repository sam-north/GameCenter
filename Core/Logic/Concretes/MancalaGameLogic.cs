using Core.Framework.Models;
using Core.Logic.Interfaces;
using Core.Mappers.Interfaces;
using Core.Models;
using Core.Models.Constants;
using Core.Models.Games;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Core.Logic.Concretes
{
    public class MancalaGameLogic : IMancalaGameLogic
    {
        private MancalaGameState _gameState;
        private ICollection<GameInstanceUser> _gameInstanceUsers;

        ICollection<string> _feedbackMessages = new List<string>();
        private IGameInstanceMapper GameInstanceMapper { get; }

        public MancalaGameLogic(IGameInstanceMapper gameInstanceMapper)
        {
            GameInstanceMapper = gameInstanceMapper;
        }
        public string GetDefaultGameState(GameInstance gameInstance)
        {
            _gameInstanceUsers = gameInstance.Users;
            SetupGame();
            return CreateGameStateAsString();
        }

        public Response<string> LoadAndPlayAndReturnGameStateAsString(GameInstance gameInstanceWithPreviousState, int userId, string userInput)
        {
            _gameInstanceUsers = gameInstanceWithPreviousState.Users;

            var response = new Response<string>();
            LoadGame(gameInstanceWithPreviousState);
            Play(userId, userInput);
            return response;
        }

        private void Play(int userId, string userInput)
        {
            if (_gameState == null || (_gameState != null && !_gameState.HasGameBeenSetup))
                SetupGame();
            if (_gameState.GameIsPlayable)
            {
                CheckGameState(userId, userInput);
                PrintPlayerBoard();
                CreateGameStateAsString();
            }
        }

        private void LoadGame(GameInstance gameInstanceWithPreviousState)
        {
            if (gameInstanceWithPreviousState != null)
            {
                _gameState = JsonSerializer.Deserialize<MancalaGameState>(gameInstanceWithPreviousState.State.DataAsJson);
                if (_gameState.HasGameBeenSetup && !_gameState.GameIsPlayable)
                    _gameState = null;
            }
        }

        private string CreateGameStateAsString()
        {
            string jsonString = JsonSerializer.Serialize(_gameState);
            return jsonString;
        }

        private void CheckGameState(int userId, string input)
        {
            if (_gameState.GameIsPlayable) Turn(userId, input);
            else AnnounceWinner();
        }

        private void AnnounceWinner()
        {
            var winner = (_gameState.Player1.Board[6] > _gameState.Player2.Board[6]) ? _gameState.Player1 : (_gameState.Player2.Board[6] > _gameState.Player1.Board[6]) ? _gameState.Player2 : null;

            if (winner != null)
            {

            }
            _feedbackMessages.Add(winner + " wins!");
            _feedbackMessages.Add($"{_gameState.Player1.User} had {_gameState.Player1.Board[6]} and {_gameState.Player2.User} had {_gameState.Player2.Board[6]}");
        }

        private void SetupGame()
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
            _feedbackMessages.Add("Welcome to Mancala!");
        }

        private MancalaPlayer CreatePlayer(GameInstanceUser gameInstanceUser)
        {
            var player = new MancalaPlayer();
            player.Board = new int[7] { 4, 4, 4, 4, 4, 4, 0 };
            player.User = GameInstanceMapper.Map(gameInstanceUser);
            return player;
        }

        private void Turn(int userId, string input)
        {
            _gameState.GameIsPlayable = CheckForEndOfGame();
            if (!_gameState.GameIsPlayable) return;

            if (userId == 0 || UserIsInvalidUser(userId) || string.IsNullOrWhiteSpace(input))
            {
                _feedbackMessages.Add("Invalid user");
                return;
            }

            var currentPlayer = GetCurrentPlayer();
            var opponentPlayer = GetOpponentPlayer();
            if (userId != currentPlayer.User.UserId)
            {
                _feedbackMessages.Add("It's not your turn!");
                return;
            }

            var playerPossibleSpots = GetPlayerPossibleSpotsToMove(currentPlayer);
            if (!(short.TryParse(input, out var indexToMove) && playerPossibleSpots.Contains(indexToMove)))
            {
                _feedbackMessages.Add($"Invalid move. Choose from: {string.Join(",", playerPossibleSpots)}");
                return;
            }

            Move(indexToMove - 1, currentPlayer, opponentPlayer);
        }

        private bool UserIsInvalidUser(int userId)
        {
            return !_gameInstanceUsers.Select(x => x.UserId).Any(x => x == userId);
        }

        private bool CheckForEndOfGame()
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

        private void PrintPlayerBoard()
        {
            var currentPlayer = GetCurrentPlayer();
            var opponentPlayer = GetOpponentPlayer();
            _feedbackMessages.Add($"{currentPlayer.User} it is your turn.");
            var feedbackMessage = new StringBuilder();
            for (int i = 6; i >= 0; i--)
            {
                if (i != 6)
                    feedbackMessage.Append(",");
                var valueString = opponentPlayer.Board[i] > 0 ? opponentPlayer.Board[i].ToString() : " ";
                feedbackMessage.Append($"[{valueString}]");
                if (i == 0)
                {
                    var opponentScoreDigits = currentPlayer.Board[6].ToString().Length;
                    feedbackMessage.Append(",");
                    feedbackMessage.Append($"[{string.Empty.PadRight(opponentScoreDigits)}]");
                }
            }
            _feedbackMessages.Add(feedbackMessage.ToString());
            feedbackMessage.Clear();
            for (int i = 0; i < currentPlayer.Board.Length; i++)
            {
                if (i == 0)
                {
                    var opponentScoreDigits = opponentPlayer.Board[6].ToString().Length;
                    feedbackMessage.Append($"[{string.Empty.PadRight(opponentScoreDigits)}]");
                    feedbackMessage.Append(",");
                }
                var valueString = currentPlayer.Board[i] > 0 ? currentPlayer.Board[i].ToString() : " ";
                feedbackMessage.Append($"[{valueString}]");
                if (i != 6)
                    feedbackMessage.Append(",");
            }
            _feedbackMessages.Add(feedbackMessage.ToString());
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
