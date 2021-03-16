using Core.Logic.Concretes;
using Core.Logic.Interfaces;
using Core.Mappers.Interfaces;
using Core.Models;
using Core.Models.Constants;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Core.Tests
{
    [TestClass]
    public class MancalaGameLogicTests
    {
        IMancalaGameLogic _mancalaGameLogic { get; }
        GameInstance _gameInstance { get; set; }
        public MancalaGameLogicTests()
        {
            var gameInstanceMapper = A.Fake<IGameInstanceMapper>();
            _mancalaGameLogic = new MancalaGameLogic(gameInstanceMapper);
        }

        [TestMethod]
        public void GetDefaultGameState_WithExpectedParameters_ShouldReturnDefaultStateAsString()
        {
            //arrange
            Arrange();

            //act
            var result = _mancalaGameLogic.GetDefaultGameState(_gameInstance);

            //assert
            Assert.AreEqual("{\"HasGameBeenSetup\":true,\"GameIsPlayable\":true,\"IsPlayer1Turn\":true,\"Player1\":{\"Board\":[4,4,4,4,4,4,0],\"User\":{\"UserId\":0,\"Role\":null,\"UserEmail\":null}},\"Player2\":{\"Board\":[4,4,4,4,4,4,0],\"User\":{\"UserId\":0,\"Role\":null,\"UserEmail\":null}}}", result);
        }

        [TestMethod]
        public void GetDefaultGameState_WithExpectedParameters_ShouldBeAbleToBeParsedToMancalaState()
        {
            //arrange
            Arrange();

            //act
            var result = _mancalaGameLogic.GetDefaultGameState(_gameInstance);

            //assert
            Assert.AreEqual("{\"HasGameBeenSetup\":true,\"GameIsPlayable\":true,\"IsPlayer1Turn\":true,\"Player1\":{\"Board\":[4,4,4,4,4,4,0],\"User\":{\"UserId\":0,\"Role\":null,\"UserEmail\":null}},\"Player2\":{\"Board\":[4,4,4,4,4,4,0],\"User\":{\"UserId\":0,\"Role\":null,\"UserEmail\":null}}}", result);
        }

        private void Arrange()
        {
            _gameInstance = new GameInstance();
            _gameInstance.Id = Guid.NewGuid();
            _gameInstance.GameId = (int)Games.Mancala;
            _gameInstance.DateCreated = DateTimeOffset.Now;
            _gameInstance.IsDeleted = false;

            var gameInstanceUsers = new List<GameInstanceUser>();
            gameInstanceUsers.Add(new GameInstanceUser
            {
                UserId = 398,
                Role = GameInstanceRoles.Player.ToString()
            });
            gameInstanceUsers.Add(new GameInstanceUser
            {
                UserId = 66246,
                Role = GameInstanceRoles.Player.ToString()
            });
            _gameInstance.Users = gameInstanceUsers;
        }
    }
}
