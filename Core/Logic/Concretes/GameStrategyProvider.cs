using Core.Logic.Interfaces;
using Core.Models.Constants;
using System;

namespace Core.Logic.Concretes
{
    public class GameStrategyProvider : IGameStrategyProvider
    {
        public IGameStrategy Provide(int gameId)
        {
            switch (gameId)
            {
                case (int)Games.Mancala:
                    return new MancalaGameLogic();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
