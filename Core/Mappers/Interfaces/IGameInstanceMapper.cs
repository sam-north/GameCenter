using Core.Models;
using Core.Models.Dtos;

namespace Core.Mappers.Interfaces
{
    public interface IGameInstanceMapper
    {
        GameInstanceDto Map(GameInstance source);
        GameInstance Map(GameInstanceDto source);
    }
}
