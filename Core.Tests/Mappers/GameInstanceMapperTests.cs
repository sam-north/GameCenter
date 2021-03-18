using Core.Framework.Models;
using Core.Mappers.Concretes;
using Core.Mappers.Interfaces;
using Core.Models;
using Core.Models.Constants;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Core.Tests.Mappers
{
    [TestClass]
    public class GameInstanceMapperTests
    {
        IGameInstanceMapper _gameInstanceMapper { get; set; }
        GameInstance _gameInstance { get; set; }
        GameInstanceUser _gameInstanceUser { get; set; }

        private void Arrange()
        {
            var fakeRequestContext = A.Fake<IRequestContext>();
            _gameInstanceMapper = new GameInstanceMapper(fakeRequestContext);

            _gameInstance = new GameInstance
            {
                Id = Guid.Parse("d3738777-44b9-4ce8-8bf7-3ce2e985ef52"),
                GameId = (int)Games.Mancala,
                DateCreated = Convert.ToDateTime("12-11-2012 12:12:12AM"),
                IsDeleted = false,
                Game = new Game
                {
                    DisplayName = Games.Mancala.ToString(),
                    Id = 1,
                    IsDeleted = false,
                    ReferenceName = Games.Mancala.ToString()
                }
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
                DataAsJson = "{\"HasGameBeenSetup\":true,\"GameIsPlayable\":true,\"IsPlayer1Turn\":true,\"Player1\":{\"Board\":[4,4,4,4,4,4,0],\"User\":{\"UserId\":398,\"Role\":\"Player\",\"UserEmail\":null}},\"Player2\":{\"Board\":[4,4,4,4,4,4,0],\"User\":{\"UserId\":66246,\"Role\":\"Player\",\"UserEmail\":null}}}",
                DateCreated = Convert.ToDateTime("12-12-2012 12:12:12AM"),
                GameInstanceId = Guid.Parse("d3738777-44b9-4ce8-8bf7-3ce2e985ef52"),
                Id = 3983
            };
            _gameInstance.Users = gameInstanceUsers;

            _gameInstanceUser = new GameInstanceUser
            {
                GameInstanceId = Guid.Parse("d3738777-44b9-4ce8-8bf7-3ce2e985ef52"),
                Id = 15,
                Role = GameInstanceRoles.Player.ToString(),
                UserId = 938,
                User = new User
                {
                    Id = 938,
                    Email = "Ttttest@tttes.com",
                    IsDeleted = true,
                    Password = "things"
                }
            };
        }

        [TestMethod]
        public void MapGameInstanceUserToDto_WithExpectedParameters_ShouldReturnExpectedMapping()
        {
            //arrange
            Arrange();

            //act
            var result = _gameInstanceMapper.Map(_gameInstanceUser);

            //assert
            Assert.AreEqual(GameInstanceRoles.Player.ToString(), result.Role);
            Assert.AreEqual("Ttttest@tttes.com", result.UserEmail);
            Assert.AreEqual(938, result.UserId);
        }

        [TestMethod]
        public void MapGameInstanceUserToDto_WithNullParameter_ShouldThrowException()
        {
            //arrange
            Arrange();
            _gameInstanceUser = null;

            //act & assert
            Assert.ThrowsException<NullReferenceException>(() => _gameInstanceMapper.Map(_gameInstanceUser));
        }

        [TestMethod]
        public void MapGameInstanceUserToDto_WithNullUser_ShouldMapNullEmailWithoutException()
        {
            //arrange
            Arrange();
            _gameInstanceUser.User = null;

            //act
            var result = _gameInstanceMapper.Map(_gameInstanceUser);

            //assert
            Assert.IsNull(result.UserEmail);
        }

        [TestMethod]
        public void MapUserGameInstance_WithExpectedParameters_ShouldReturnExpectedMapping()
        {
            //arrange
            Arrange();

            //act
            var result = _gameInstanceMapper.Map(_gameInstance);

            //assert
            Assert.AreEqual(Guid.Parse("d3738777-44b9-4ce8-8bf7-3ce2e985ef52"), result.Id);
            Assert.AreEqual("Mancala", result.GameDisplayName);
            Assert.AreEqual(Convert.ToDateTime("12-11-2012 12:12:12AM"), result.DateCreated);
            Assert.AreEqual((int)Games.Mancala, result.GameId);

            Assert.IsNotNull(result.State);
            Assert.IsNotNull(result.Users);
            Assert.AreEqual(4, result.Users.Count);
        }
    }
}
