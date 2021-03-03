namespace Core.Logic.Interfaces
{
    public interface IGameStrategyProvider
    {
        IGameStrategy Provide(int gameId);
    }
}
