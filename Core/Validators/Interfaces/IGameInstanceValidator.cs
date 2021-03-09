using Core.Framework.Models;
using Core.Models.Dtos;

namespace Core.Validators.Interfaces
{
    public interface IGameInstanceValidator
    {
        Response<string> Validate(CreateGameInstanceDto dto);
        Response<string> Validate(PlayGameInstanceDto dto);
    }
}
