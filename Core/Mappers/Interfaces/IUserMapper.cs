using Core.Models;
using Core.Models.Dtos;

namespace Core.Mappers.Interfaces
{
    public interface IUserMapper
    {
        User Map(UserSignUpDto dto);
        User Map(UserSignInDto dto);
    }
}
