using Core.Logic.Interfaces;
using Core.Models.Constants;
using Core.Providers.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.Providers.Concretes
{
    public class GameStrategyProvider : IGameStrategyProvider
    {
        public IServiceProvider ServiceProvider { get; }

        public GameStrategyProvider(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public IGameStrategy Provide(int gameId)
        {
            switch (gameId)
            {
                case (int)Games.Mancala:
                    return ServiceProvider.GetService<IMancalaGameLogic>();
                case (int)Games.Checkers:
                    return ServiceProvider.GetService<ICheckersGameLogic>();
                case (int)Games.ConnectFour:
                    return ServiceProvider.GetService<IConnectFourGameLogic>();
                case (int)Games.TicTacToe:
                    return ServiceProvider.GetService<ITicTacToeGameLogic>();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
