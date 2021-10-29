using Core.Models.Interfaces;

namespace Core.Models.Games
{
    public class TicTacToeGameState : IGameState
    {

        public bool HasGameBeenSetup { get; set; }

        public string[] Board { get; set; }

        public string Winner { get; set; }

        public bool xIsNext { get; set; }

        public string Player1 { get; set; }

        public string Player2 { get; set; }


        public bool GameIsPlayable { get; set; }
        public string Result { get; set; }
    }
}
