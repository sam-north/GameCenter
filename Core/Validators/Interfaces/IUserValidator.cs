using Core.Framework.Models;
using Core.Models.Dtos;

namespace Core.Validators.Interfaces
{
    public interface IUserValidator
    {
        Response<string> Validate(UserSignUpDto dto);
        Response<string> Validate(UserSignInDto dto);
    }
}