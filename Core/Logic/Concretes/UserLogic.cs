using Core.DataAccess;
using Core.Framework.Models;
using Core.Logic.Interfaces;
using Core.Models;
using CryptSharp;
using System.Linq;
using System.Text;

namespace Core.Logic.Concretes
{
    public class UserLogic : IUserLogic
    {
        public ModelContext ModelContext { get; }
        public UserLogic(ModelContext modelContext)
        {
            ModelContext = modelContext;
        }

        public User Create(User model)
        {
            model.Password = HashPassword(model.Password, Crypter.Blowfish.GenerateSalt(6));

            ModelContext.Users.Add(model);
            ModelContext.SaveChanges();

            return model;
        }

        public User Get(string email)
        {
            return ModelContext.Users.SingleOrDefault(x => x.Email == email);
        }

        public Response<User> SignIn(User model)
        {
            var response = new Response<User>();
            var dbUser = Get(model.Email);
            if (dbUser == null)
            {
                response.Errors.Add("Invalid email or password");
                return response;
            }

            bool matches = VerifyHash(dbUser.Password, model.Password);
            if (!matches)
            {
                response.Errors.Add("Invalid email or password");
                return response;
            }

            response.Data = dbUser;
            return response;
        }

        private string HashPassword(string password, string salt)
        {
            return Crypter.Blowfish.Crypt(Encoding.ASCII.GetBytes(password), salt);
        }

        private bool VerifyHash(string existingHash, string attemptedPassword)
        {
            return Crypter.SafeEquals(existingHash, HashPassword(attemptedPassword, existingHash));
        }
    }
}
