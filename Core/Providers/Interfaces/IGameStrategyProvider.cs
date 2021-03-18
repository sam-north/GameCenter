using Core.Logic.Interfaces;

namespace Core.Providers.Interfaces
{
    public interface IGameStrategyProvider
    {
        IGameStrategy Provide(int gameId);
    }
}
