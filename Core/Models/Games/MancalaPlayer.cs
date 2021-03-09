using Core.Models.Dtos;

namespace Core.Models.Games
{
    public class MancalaPlayer
    {
        public MancalaPlayer()
        {
            Board = new int[7];
        }

        public int[] Board { get; set; }
        public GameInstanceUserDto User { get; set; }
    }
}
