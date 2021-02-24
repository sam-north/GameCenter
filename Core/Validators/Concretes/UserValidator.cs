using Core.Framework.Models;
using Core.Framework.Validators;
using Core.Models.Dtos;
using Core.Validators.Interfaces;

namespace Core.Validators.Concretes
{
    public class UserValidator : IUserValidator
    {
        public Response<string> Validate(UserSignUpDto dto)
        {
            var response = new Response<string>();

            if (string.IsNullOrWhiteSpace(dto.Email))
                response.Errors.Add("Email is required");
            else if (!Validation.IsValidEmail(dto.Email))
                response.Errors.Add("Email must be an email address.");

            if (string.IsNullOrWhiteSpace(dto.Password))
                response.Errors.Add("Password is required");
            else if (dto.Password.Length < 8)
                response.Errors.Add("Password must be at least 8 characters in length.");

            if (dto.Password != dto.ConfirmPassword)
                response.Errors.Add("Password and confirm password must match.");

            return response;
        }
        public Response<string> Validate(UserSignInDto dto)
        {
            var response = new Response<string>();

            if (string.IsNullOrWhiteSpace(dto.Email))
                response.Errors.Add("Email is required");

            if (string.IsNullOrWhiteSpace(dto.Password))
                response.Errors.Add("Password is required");

            return response;
        }
    }
}
