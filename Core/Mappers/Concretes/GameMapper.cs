using Core.Mappers.Interfaces;
using Core.Models;
using Core.Models.Dtos;
using System.Collections.Generic;

namespace Core.Mappers.Concretes
{
    public class GameMapper : IGameMapper
    {
        private GameDto Map(Game source)
        {
            var target = new GameDto();
            target.DisplayName = source.DisplayName;
            target.Id = source.Id;
            return target;
        }

        public ICollection<GameDto> Map(ICollection<Game> source)
        {
            var targets = new List<GameDto>();
            foreach (var game in source)
                targets.Add(Map(game));
            return targets;
        }
    }
}
