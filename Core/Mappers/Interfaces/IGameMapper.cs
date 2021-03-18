using Core.Models;
using Core.Models.Dtos;
using System.Collections.Generic;

namespace Core.Mappers.Interfaces
{
    public interface IGameMapper
    {
        ICollection<GameDto> Map(ICollection<Game> source);
    }
}
