using Core.Framework.Models;
using Core.Mappers.Concretes;
using Core.Mappers.Interfaces;
using Core.Models;
using Core.Models.Constants;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Core.Tests.Mappers
{
    [TestClass]
    public class GameInstanceMapperTests
    {
        IGameInstanceMapper _gameInstanceMapper { get; set; }
        GameInstanceUser _gameInstanceUser { get; set; }

        private void Arrange()
        {
            var fakeRequestContext = A.Fake<IRequestContext>();
            _gameInstanceMapper = new GameInstanceMapper(fakeRequestContext);

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
    }
}
