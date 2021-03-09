namespace Core.Models.Games
{
    public class MancalaGameState
    {
        public bool HasGameBeenSetup { get; set; }
        public bool GameIsPlayable { get; set; }
        public bool IsPlayer1Turn { get; set; }
        public MancalaPlayer Player1 { get; set; }
        public MancalaPlayer Player2 { get; set; }
    }
}
