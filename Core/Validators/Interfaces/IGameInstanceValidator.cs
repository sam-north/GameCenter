using Core.Framework.Models;
using Core.Models.Dtos;

namespace Core.Validators.Interfaces
{
    public interface IGameInstanceValidator
    {
        IResponse<string> Validate(CreateGameInstanceDto dto);
        IResponse<string> Validate(PlayGameInstanceDto dto);
    }
}
