using Core.Logic.Interfaces;
using Core.Providers.Concretes;
using Core.Providers.Interfaces;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Core.Tests.Logic
{
    [TestClass]
    public class GameStrategyProviderTests
    {
        IGameStrategyProvider _gameStrategyProvider { get; set; }

        [TestMethod]
        private void Arrange()
        {
            var serviceProvider = A.Fake<IServiceProvider>();
            A.CallTo(() => serviceProvider.GetService(typeof(IMancalaGameLogic))).Returns(A.Fake<IMancalaGameLogic>());
            _gameStrategyProvider = new GameStrategyProvider(serviceProvider);
        }
    }
}
