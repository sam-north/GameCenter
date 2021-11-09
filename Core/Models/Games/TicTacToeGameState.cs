using Core.Models.Interfaces;

namespace Core.Models.Games
{
    public class TicTacToeGameState : IGameState
    {
        public bool HasGameBeenSetup { get; set; }
        public string[] Board { get; set; }
        public bool IsPlayer1Turn { get; set; }
        public bool GameIsPlayable { get; set; }
        public string Result { get; set; }

        public TicTacToePlayer Player1 { get; set; }
        public TicTacToePlayer Player2 { get; set; }
    }
}