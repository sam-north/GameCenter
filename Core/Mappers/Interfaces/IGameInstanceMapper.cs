using Core.Models;
using Core.Models.Dtos;
using System.Collections.Generic;

namespace Core.Mappers.Interfaces
{
    public interface IGameInstanceMapper
    {
        GameInstanceDto Map(GameInstance source);
        GameInstance Map(GameInstanceDto source);
        GameInstanceUserDto Map(GameInstanceUser gameInstanceUser);
        ICollection<UserGameInstanceStatelessDto> Map(ICollection<GameInstance> gameInstances);
    }
}
