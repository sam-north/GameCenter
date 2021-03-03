using Core.Mappers.Interfaces;
using Core.Models;
using Core.Models.Dtos;

namespace Core.Mappers.Concretes
{
    public class UserMapper : IUserMapper
    {
        public User Map(UserSignUpDto source)
        {
            var target = new User();
            target.Email = source.Email;
            target.Password = source.Password;
            return target;
        }

        public User Map(UserSignInDto source)
        {
            var target = new User();
            target.Email = source.Email;
            target.Password = source.Password;
            return target;
        }
    }
}
