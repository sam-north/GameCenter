using Core.Mappers.Interfaces;
using Core.Models;
using Core.Models.Dtos;

namespace Core.Mappers.Concretes
{
    public class UserMapper : IUserMapper
    {
        public User Map(UserSignUpDto dto)
        {
            var entity = new User();
            entity.Email = dto.Email;
            entity.Password = dto.Password;
            return entity;
        }

        public User Map(UserSignInDto dto)
        {
            var entity = new User();
            entity.Email = dto.Email;
            entity.Password = dto.Password;
            return entity;
        }
    }
}
