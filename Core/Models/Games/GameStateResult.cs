using Core.Models.Interfaces;

namespace Core.Models.Games
{
    public class GameStateResult : IGameState
    {
        public string Result { get; set; }
        public bool GameIsPlayable { get; set; }
    }
}
