using Core.Framework.Serializers;
using Core.Logic.Concretes;
using Core.Logic.Interfaces;
using Core.Mappers.Interfaces;
using Core.Models;
using Core.Models.Constants;
using Core.Models.Games;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Tests.Logic
{
    [TestClass]
    public class MancalaGameLogicTests
    {
        IMancalaGameLogic _mancalaGameLogic { get; set; }
        GameInstance _gameInstance { get; set; }

        private void Arrange()
        {
            var gameInstanceMapper = A.Fake<IGameInstanceMapper>();
            _mancalaGameLogic = new MancalaGameLogic(gameInstanceMapper);

            _gameInstance = new GameInstance
            {
                Id = Guid.Parse("d3738777-44b9-4ce8-8bf7-3ce2e985ef52"),
                GameId = (int)Games.Mancala,
                DateCreated = Convert.ToDateTime("12-11-2012 12:12:12AM"),
                IsDeleted = false
            };

            var gameInstanceUsers = new List<GameInstanceUser>
            {
                new GameInstanceUser
                {
                    UserId = 398,
                    Role = GameInstanceRoles.Player.ToString()
                },
                new GameInstanceUser
                {
                    UserId = 66246,
                    Role = GameInstanceRoles.Player.ToString()
                },
                new GameInstanceUser
                {
                    UserId = 6363,
                    Role = GameInstanceRoles.Spectator.ToString()
                },
                new GameInstanceUser
                {
                    UserId = 3785633,
                    Role = GameInstanceRoles.Spectator.ToString()
                }
            };

            _gameInstance.State = new GameInstanceState
            {
                DataAsJson = "{\"hasGameBeenSetup\":true,\"gameIsPlayable\":true,\"isPlayer1Turn\":true," +
                             "\"player1\":{\"board\":[4,4,4,4,4,4,0],\"user\":{\"userId\":398,\"role\":\"player\",\"userEmail\":null}}," +
                             "\"player2\":{\"board\":[4,4,4,4,4,4,0],\"user\":{\"userId\":66246,\"role\":\"player\",\"userEmail\":null}}}",
                DateCreated = Convert.ToDateTime("12-12-2012 12:12:12AM"),
                GameInstanceId = Guid.Parse("d3738777-44b9-4ce8-8bf7-3ce2e985ef52"),
                Id = 3983
            };

            _gameInstance.Users = gameInstanceUsers;
        }

        [TestMethod]
        public void GetDefaultGameState_WithExpectedParameters_ShouldReturnDefaultStateAsString()
        {
            //arrange
            Arrange();

            //act
            var result = _mancalaGameLogic.GetDefaultGameState(_gameInstance);

            //assert
            Assert.AreEqual("{\"hasGameBeenSetup\":true,\"gameIsPlayable\":true,\"isPlayer1Turn\":true,\"player1\":{\"board\":[4,4,4,4,4,4,0],\"user\":{\"userId\":0,\"role\":null,\"userEmail\":null}},\"player2\":{\"board\":[4,4,4,4,4,4,0],\"user\":{\"userId\":0,\"role\":null,\"userEmail\":null}},\"result\":null}", result);
        }

        [TestMethod]
        public void GetDefaultGameState_WithExpectedParameters_ShouldBeAbleToBeParsedToMancalaState()
        {
            //arrange
            Arrange();

            //act
            var gameStateAsString = _mancalaGameLogic.GetDefaultGameState(_gameInstance);
            var result = JsonSerializer.Deserialize<MancalaGameState>(gameStateAsString);

            //assert
            Assert.AreEqual(true, result.HasGameBeenSetup);
            Assert.AreEqual(true, result.GameIsPlayable);
            Assert.AreEqual(true, result.IsPlayer1Turn);
            Assert.IsNotNull(result.Player1);
            var expectedPlayer1Board = new int[] { 4,4,4,4,4,4,0 };
            Assert.IsTrue(Enumerable.SequenceEqual(expectedPlayer1Board, result.Player1.Board));
            Assert.IsNotNull(result.Player2);
            var expectedPlayer2Board = new int[] { 4, 4, 4, 4, 4, 4, 0 };
            Assert.IsTrue(Enumerable.SequenceEqual(expectedPlayer2Board, result.Player2.Board));
        }

        [TestMethod]
        public void GetDefaultGameState_WithNullGameInstanceUsers_ShouldThrowException()
        {
            //arrange
            Arrange();
            _gameInstance.Users = null;

            //act & assert
            Assert.ThrowsException<ArgumentNullException>(() => _mancalaGameLogic.GetDefaultGameState(_gameInstance));
        }

        [TestMethod]
        public void GetDefaultGameState_WithNotEnoughGameInstanceUsers_ShouldThrowException()
        {
            //arrange
            Arrange();
            _gameInstance.Users = new List<GameInstanceUser>
            {
                new GameInstanceUser
                {
                    UserId = 398,
                    Role = GameInstanceRoles.Player.ToString()
                }
            };

            //act & assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _mancalaGameLogic.GetDefaultGameState(_gameInstance));
        }

        [TestMethod]
        public void GetDefaultGameState_WithNotEnoughPlayerRoleGameInstanceUsers_ShouldThrowException()
        {
            //arrange
            Arrange();
            _gameInstance.Users = new List<GameInstanceUser>
            {
                new GameInstanceUser
                {
                    UserId = 398,
                    Role = GameInstanceRoles.Spectator.ToString()
                },
                new GameInstanceUser
                {
                    UserId = 56757,
                    Role = GameInstanceRoles.Player.ToString()
                }
            };

            //act & assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _mancalaGameLogic.GetDefaultGameState(_gameInstance));
        }

        [TestMethod]
        public void GetDefaultGameState_WithNullGameInstance_ShouldThrowException()
        {
            //arrange
            Arrange();
            _gameInstance = null;

            //act & assert
            Assert.ThrowsException<NullReferenceException>(() => _mancalaGameLogic.GetDefaultGameState(_gameInstance));
        }

        [TestMethod]
        public void LoadAndPlayAndReturnGameStateAsString_WithValidParameters_ShouldReturnExpectedResultState()
        {
            //arrange
            Arrange();

            //act
            var gameStateResponse = _mancalaGameLogic.LoadAndPlayAndReturnGameStateAsString(_gameInstance, 398, "2");
            var parsedResult = JsonSerializer.Deserialize<MancalaGameState>(gameStateResponse.Data);

            //assert
            Assert.AreEqual(0, gameStateResponse.Errors.Count);
            Assert.AreEqual(0, gameStateResponse.Messages.Count);

            Assert.AreEqual(true, parsedResult.HasGameBeenSetup);
            Assert.AreEqual(true, parsedResult.GameIsPlayable);
            Assert.AreEqual(false, parsedResult.IsPlayer1Turn);
            Assert.IsNotNull(parsedResult.Player1);
            var expectedPlayer1Board = new int[] { 4, 0, 5, 5, 5, 5, 0 };
            Assert.IsTrue(Enumerable.SequenceEqual(expectedPlayer1Board, parsedResult.Player1.Board));
            Assert.IsNotNull(parsedResult.Player2);
            var expectedPlayer2Board = new int[] { 4, 4, 4, 4, 4, 4, 0 };
            Assert.IsTrue(Enumerable.SequenceEqual(expectedPlayer2Board, parsedResult.Player2.Board));
        }

        //[TestMethod]
        //public void LoadAndPlayAndReturnGameStateAsString_WithValidParameters_ShouldReturnExpectedResultStates()
        //{
        //    //arrange
        //    Arrange();
        //    _gameInstance.State = new GameInstanceState
        //    {
        //        DataAsJson = "{\"hasGameBeenSetup\":true,\"gameIsPlayable\":true,\"isPlayer1Turn\":true," +
        //        "\"player1\":{\"board\":[0,1,0,1,1,0,28],\"user\":{\"userId\":2,\"role\":\"Player\",\"userEmail\":\"dingle@dingle.com\"}}," +
        //        "\"player2\":{\"board\":[0,0,0,1,0,0,16],\"user\":{\"userId\":1,\"role\":\"Player\",\"userEmail\":\"test@test.com\"}}," +
        //        "\"result\":null}",
        //        DateCreated = Convert.ToDateTime("12-12-2012 12:12:12AM"),
        //        GameInstanceId = Guid.Parse("d3738777-44b9-4ce8-8bf7-3ce2e985ef52"),
        //        Id = 3983
        //    };
        //    _gameInstance.Users = new List<GameInstanceUser>
        //    {
        //        new GameInstanceUser
        //        {
        //            UserId = 2,
        //            Role = GameInstanceRoles.Player.ToString()
        //        },
        //        new GameInstanceUser
        //        {
        //            UserId = 1,
        //            Role = GameInstanceRoles.Player.ToString()
        //        }
        //    };

        //    //act
        //    var gameStateResponse = _mancalaGameLogic.LoadAndPlayAndReturnGameStateAsString(_gameInstance, 2, "2");
        //    var parsedResult = JsonSerializer.Deserialize<MancalaGameState>(gameStateResponse.Data);

        //    //assert
        //    Assert.AreEqual(0, gameStateResponse.Errors.Count);
        //    Assert.AreEqual(0, gameStateResponse.Messages.Count);

        //    Assert.AreEqual(true, parsedResult.HasGameBeenSetup);
        //    Assert.AreEqual(true, parsedResult.GameIsPlayable);
        //    Assert.AreEqual(false, parsedResult.IsPlayer1Turn);
        //    Assert.IsNotNull(parsedResult.Player1);
        //    var expectedPlayer1Board = new int[] { 4, 0, 5, 5, 5, 5, 0 };
        //    Assert.IsTrue(Enumerable.SequenceEqual(expectedPlayer1Board, parsedResult.Player1.Board));
        //    Assert.IsNotNull(parsedResult.Player2);
        //    var expectedPlayer2Board = new int[] { 4, 4, 4, 4, 4, 4, 0 };
        //    Assert.IsTrue(Enumerable.SequenceEqual(expectedPlayer2Board, parsedResult.Player2.Board));
        //}

        [TestMethod]
        public void LoadAndPlayAndReturnGameStateAsString_WithInvalidMove_ShouldReturnError()
        {
            //arrange
            Arrange();
            _gameInstance.State = new GameInstanceState
            {
                DataAsJson = "{\"hasGameBeenSetup\":true,\"gameIsPlayable\":true,\"isPlayer1Turn\":true,\"player1\":{\"board\":[4,0,4,4,4,4,0],\"user\":{\"userId\":398,\"role\":\"Player\",\"userEmail\":null}},\"player2\":{\"board\":[4,4,4,4,4,4,0],\"user\":{\"userId\":66246,\"role\":\"Player\",\"userEmail\":null}}}",
                DateCreated = Convert.ToDateTime("12-12-2012 12:12:12AM"),
                GameInstanceId = Guid.Parse("d3738777-44b9-4ce8-8bf7-3ce2e985ef52"),
                Id = 3983
            };

            //act
            var gameStateResponse = _mancalaGameLogic.LoadAndPlayAndReturnGameStateAsString(_gameInstance, 398, "2");
            var parsedResult = JsonSerializer.Deserialize<MancalaGameState>(gameStateResponse.Data);

            //assert
            Assert.AreEqual(1, gameStateResponse.Errors.Count);
            Assert.AreEqual("Invalid move. Choose from: 1,3,4,5,6", gameStateResponse.Errors.First());
        }

        [TestMethod]
        public void LoadAndPlayAndReturnGameStateAsString_WithError_ShouldReturnSameState()
        {
            //arrange
            Arrange();
            _gameInstance.State = new GameInstanceState
            {
                DataAsJson = "{\"hasGameBeenSetup\":true,\"gameIsPlayable\":true,\"isPlayer1Turn\":true,\"player1\":{\"board\":[4,0,3,3,3,3,1],\"user\":{\"userId\":398,\"role\":\"Player\",\"userEmail\":null}},\"player2\":{\"board\":[6,6,6,6,6,6,2],\"user\":{\"userId\":66246,\"role\":\"Player\",\"userEmail\":null}}}",
                DateCreated = Convert.ToDateTime("12-12-2012 12:12:12AM"),
                GameInstanceId = Guid.Parse("d3738777-44b9-4ce8-8bf7-3ce2e985ef52"),
                Id = 3983
            };

            //act
            var gameStateResponse = _mancalaGameLogic.LoadAndPlayAndReturnGameStateAsString(_gameInstance, 398, "2");
            var parsedResult = JsonSerializer.Deserialize<MancalaGameState>(gameStateResponse.Data);

            //assert
            Assert.AreEqual(true, parsedResult.HasGameBeenSetup);
            Assert.AreEqual(true, parsedResult.GameIsPlayable);
            Assert.AreEqual(true, parsedResult.IsPlayer1Turn);
            Assert.IsNotNull(parsedResult.Player1);
            var expectedPlayer1Board = new int[] { 4,0,3,3,3,3,1 };
            Assert.IsTrue(Enumerable.SequenceEqual(expectedPlayer1Board, parsedResult.Player1.Board));
            Assert.IsNotNull(parsedResult.Player2);
            var expectedPlayer2Board = new int[] { 6,6,6,6,6,6,2 };
            Assert.IsTrue(Enumerable.SequenceEqual(expectedPlayer2Board, parsedResult.Player2.Board));
        }

        [TestMethod]
        public void LoadAndPlayAndReturnGameStateAsString_WithWrongUserIdForThatTurn_ShouldReturnError()
        {
            //arrange
            Arrange();

            //act
            var gameStateResponse = _mancalaGameLogic.LoadAndPlayAndReturnGameStateAsString(_gameInstance, 66246, "2");
            var parsedResult = JsonSerializer.Deserialize<MancalaGameState>(gameStateResponse.Data);

            //assert
            Assert.AreEqual(1, gameStateResponse.Errors.Count);
            Assert.AreEqual("It's not your turn!", gameStateResponse.Errors.First());
        }

        [TestMethod]
        public void LoadAndPlayAndReturnGameStateAsString_WithInvalidUserId_ShouldReturnError()
        {
            //arrange
            Arrange();

            //act
            var gameStateResponse = _mancalaGameLogic.LoadAndPlayAndReturnGameStateAsString(_gameInstance, 5783, "2");
            var parsedResult = JsonSerializer.Deserialize<MancalaGameState>(gameStateResponse.Data);

            //assert
            Assert.AreEqual(1, gameStateResponse.Errors.Count);
            Assert.AreEqual("Invalid user", gameStateResponse.Errors.First());
        }

        [TestMethod]
        public void LoadAndPlayAndReturnGameStateAsString_WithEmptyUserId_ShouldReturnError()
        {
            //arrange
            Arrange();

            //act
            var gameStateResponse = _mancalaGameLogic.LoadAndPlayAndReturnGameStateAsString(_gameInstance, 0, "2");
            var parsedResult = JsonSerializer.Deserialize<MancalaGameState>(gameStateResponse.Data);

            //assert
            Assert.AreEqual(1, gameStateResponse.Errors.Count);
            Assert.AreEqual("Invalid user", gameStateResponse.Errors.First());
        }

        [TestMethod]
        public void LoadAndPlayAndReturnGameStateAsString_WithEmptyMoveInput_ShouldReturnError()
        {
            //arrange
            Arrange();

            //act
            var gameStateResponse = _mancalaGameLogic.LoadAndPlayAndReturnGameStateAsString(_gameInstance, 398, "");
            var parsedResult = JsonSerializer.Deserialize<MancalaGameState>(gameStateResponse.Data);

            //assert
            Assert.AreEqual(1, gameStateResponse.Errors.Count);
            Assert.AreEqual("Invalid move. Choose from: 1,2,3,4,5,6", gameStateResponse.Errors.First());
        }

        [TestMethod]
        public void LoadAndPlayAndReturnGameStateAsString_WithGameNotPlayableAndTied_ShouldAnnounceTie()
        {
            //arrange
            Arrange();
            _gameInstance.State = new GameInstanceState
            {
                DataAsJson = "{\"hasGameBeenSetup\":true,\"gameIsPlayable\":false,\"isPlayer1Turn\":true,\"player1\":{\"board\":[4,0,3,3,3,3,33],\"user\":{\"userId\":398,\"role\":\"Player\",\"userEmail\":null}},\"player2\":{\"board\":[6,6,6,6,6,6,33],\"user\":{\"userId\":66246,\"role\":\"Player\",\"userEmail\":null}}}",
                DateCreated = Convert.ToDateTime("12-12-2012 12:12:12AM"),
                GameInstanceId = Guid.Parse("d3738777-44b9-4ce8-8bf7-3ce2e985ef52"),
                Id = 3983
            };

            //act
            var gameStateResponse = _mancalaGameLogic.LoadAndPlayAndReturnGameStateAsString(_gameInstance, 398, "2");
            var parsedResult = JsonSerializer.Deserialize<MancalaGameState>(gameStateResponse.Data);

            //assert
            Assert.AreEqual(1, gameStateResponse.Messages.Count);
            Assert.AreEqual("It's a tie! Nobody wins!", gameStateResponse.Messages.First());
        }

        [TestMethod]
        public void LoadAndPlayAndReturnGameStateAsString_WithGameEndedAndPlayer1Wins_ShouldAnnounceWinner()
        {
            //arrange
            Arrange();
            _gameInstance.State = new GameInstanceState
            {
                DataAsJson = "{\"hasGameBeenSetup\":true,\"gameIsPlayable\":false,\"isPlayer1Turn\":true,\"player1\":{\"board\":[4,0,3,3,3,3,44],\"user\":{\"userId\":398,\"role\":\"Player\",\"userEmail\":\"dingleberry@dingle.com\"}},\"player2\":{\"board\":[6,6,6,6,6,6,23],\"user\":{\"userId\":66246,\"role\":\"Player\",\"userEmail\":\"hipster@shorts.com\"}}}",
                DateCreated = Convert.ToDateTime("12-12-2012 12:12:12AM"),
                GameInstanceId = Guid.Parse("d3738777-44b9-4ce8-8bf7-3ce2e985ef52"),
                Id = 3983
            };

            //act
            var gameStateResponse = _mancalaGameLogic.LoadAndPlayAndReturnGameStateAsString(_gameInstance, 398, "2");
            var parsedResult = JsonSerializer.Deserialize<MancalaGameState>(gameStateResponse.Data);

            //assert
            Assert.AreEqual(2, gameStateResponse.Messages.Count);
            Assert.AreEqual("dingleberry@dingle.com wins!", gameStateResponse.Messages.ElementAt(0));
            Assert.AreEqual("dingleberry@dingle.com had 44 and hipster@shorts.com had 23", gameStateResponse.Messages.ElementAt(1));
        }

        [TestMethod]
        public void LoadAndPlayAndReturnGameStateAsString_WithGameEndedAndPlayer2Wins_ShouldAnnounceWinner()
        {
            //arrange
            Arrange();
            _gameInstance.State = new GameInstanceState
            {
                DataAsJson = "{\"hasGameBeenSetup\":true,\"gameIsPlayable\":false,\"isPlayer1Turn\":true,\"player1\":{\"board\":[4,0,3,3,3,3,12],\"user\":{\"userId\":398,\"role\":\"Player\",\"userEmail\":\"dingleberry@dingle.com\"}},\"player2\":{\"board\":[6,6,6,6,6,6,19],\"user\":{\"userId\":66246,\"role\":\"Player\",\"userEmail\":\"hipster@shorts.com\"}}}",
                DateCreated = Convert.ToDateTime("12-12-2012 12:12:12AM"),
                GameInstanceId = Guid.Parse("d3738777-44b9-4ce8-8bf7-3ce2e985ef52"),
                Id = 3983
            };

            //act
            var gameStateResponse = _mancalaGameLogic.LoadAndPlayAndReturnGameStateAsString(_gameInstance, 398, "2");
            var parsedResult = JsonSerializer.Deserialize<MancalaGameState>(gameStateResponse.Data);

            //assert
            Assert.AreEqual(2, gameStateResponse.Messages.Count);
            Assert.AreEqual("hipster@shorts.com wins!", gameStateResponse.Messages.ElementAt(0));
            Assert.AreEqual("dingleberry@dingle.com had 12 and hipster@shorts.com had 19", gameStateResponse.Messages.ElementAt(1));
        }

        [TestMethod]
        public void LoadAndPlayAndReturnGameStateAsString_WithNullGameInstance_ShouldThrowException()
        {
            //arrange
            Arrange();
            _gameInstance = null;

            //act & assert
            Assert.ThrowsException<NullReferenceException>(() => _mancalaGameLogic.LoadAndPlayAndReturnGameStateAsString(_gameInstance, 398, "2"));
        }

        [TestMethod]
        public void LoadAndPlayAndReturnGameStateAsString_WithNullGameInstanceState_ShouldThrowException()
        {
            //arrange
            Arrange();
            _gameInstance.State = null;

            //act & assert
            Assert.ThrowsException<NullReferenceException>(() => _mancalaGameLogic.LoadAndPlayAndReturnGameStateAsString(_gameInstance, 398, "2"));
        }

        [TestMethod]
        public void LoadAndPlayAndReturnGameStateAsString_WithNullGameInstanceUsers_ShouldThrowException()
        {
            //arrange
            Arrange();
            _gameInstance.Users = null;

            //act & assert
            Assert.ThrowsException<ArgumentNullException>(() => _mancalaGameLogic.LoadAndPlayAndReturnGameStateAsString(_gameInstance, 398, "2"));
        }
    }
}
