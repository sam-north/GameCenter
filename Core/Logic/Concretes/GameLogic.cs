using Core.DataAccess;
using Core.Logic.Interfaces;
using Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Core.Logic.Concretes
{
    public class GameLogic : IGameLogic
    {
        private ModelContext ModelContext { get; }

        public GameLogic(ModelContext modelContext)
        {
            ModelContext = modelContext;
        }

        public ICollection<Game> GetActiveGames()
        {
            return ModelContext.Games.Where(x => !x.IsDeleted).OrderBy(x => x.DisplayName).ToList();
        }
    }
}
