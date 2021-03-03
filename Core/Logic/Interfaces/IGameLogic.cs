using Core.Models;
using System.Collections.Generic;

namespace Core.Logic.Interfaces
{
    public interface IGameLogic
    {
        ICollection<Game> GetActiveGames();
    }
}
