namespace Core.Models.Games
{
    class TicTacToeGameState
    {

        public bool HasGameBeenSetup { get; set; }

        public string[] Board { get; set; }

        public string Winner { get; set; }

        public bool xIsNext { get; set; }

        public string Player1 { get; set; }

        public string Player2 { get; set; }
    }
}
