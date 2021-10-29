namespace Core.Models.Interfaces
{
    public interface IGameState
    {
        public bool GameIsPlayable { get; set; }
        public string Result { get; set; }
    }
}
