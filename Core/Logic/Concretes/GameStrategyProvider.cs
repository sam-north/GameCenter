using Core.Logic.Interfaces;
using Core.Models.Constants;
using System;

namespace Core.Logic.Concretes
{
    public class GameStrategyProvider : IGameStrategyProvider
    {
        public IMancalaGameLogic MancalaGameLogic { get; }
        public GameStrategyProvider(IMancalaGameLogic mancalaGameLogic)
        {
            MancalaGameLogic = mancalaGameLogic;
        }

        public IGameStrategy Provide(int gameId)
        {
            switch (gameId)
            {
                case (int)Games.Mancala:
                    return MancalaGameLogic;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
