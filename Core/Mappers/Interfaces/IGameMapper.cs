using Core.Models;
using Core.Models.Dtos;

namespace Core.Mappers.Interfaces
{
    public interface IGameMapper
    {
        GameDto Map(Game source);
    }
}
