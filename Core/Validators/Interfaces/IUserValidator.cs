using Core.Framework.Models;
using Core.Models.Dtos;

namespace Core.Validators.Interfaces
{
    public interface IUserValidator
    {
        IResponse<string> Validate(UserSignUpDto dto);
        IResponse<string> Validate(UserSignInDto dto);
    }
}