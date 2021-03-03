using Core.Mappers.Interfaces;
using Core.Models;
using Core.Models.Dtos;

namespace Core.Mappers.Concretes
{
    public class GameMapper : IGameMapper
    {
        public GameDto Map(Game source)
        {
            var target = new GameDto();
            target.DisplayName = source.DisplayName;
            target.Id = source.Id;
            return target;
        }
    }
}
