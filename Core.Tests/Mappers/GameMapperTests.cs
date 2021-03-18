using Core.Mappers.Concretes;
using Core.Mappers.Interfaces;
using Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Tests.Mappers
{
    [TestClass]
    public class GameMapperTests
    {
        IGameMapper _gameMapper { get; set; }
        ICollection<Game> _games { get; set; }

        private void Arrange()
        {
            _gameMapper = new GameMapper();

            _games = new List<Game>();
            _games.Add(new Game
            {
                Id = 23,
                DisplayName = "nems",
                IsDeleted = false,
                ReferenceName = "nemees"
            });
            _games.Add(new Game
            {
                Id = 673745,
                DisplayName = "qwqeqwe",
                IsDeleted = true,
                ReferenceName = "qwwq"
            });
        }

        [TestMethod]
        public void MapGames_WithExpectedParameters_ShouldMapAll()
        {
            //arrange
            Arrange();

            //act
            var result = _gameMapper.Map(_games);

            //assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("nems", result.ElementAt(0).DisplayName);
            Assert.AreEqual(23, result.ElementAt(0).Id);
            Assert.AreEqual("qwqeqwe", result.ElementAt(1).DisplayName);
            Assert.AreEqual(673745, result.ElementAt(1).Id);
        }

        [TestMethod]
        public void MapGames_WithNullParameters_ShouldThrowException()
        {
            //arrange
            Arrange();
            _games = null;

            //act & assert
            Assert.ThrowsException<NullReferenceException>(() => _gameMapper.Map(_games));
        }

        [TestMethod]
        public void MapGames_WithEmptyParameters_ShouldReturnEmptyCollection()
        {
            //arrange
            Arrange();
            _games = new List<Game>();

            //act
            var result = _gameMapper.Map(_games);

            //assert
            Assert.IsFalse(result.Any());
        }
    }
}
