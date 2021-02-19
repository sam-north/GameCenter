using Core.DataAccess;
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
            model.Salt = Crypter.Blowfish.GenerateSalt();
            model.Password = HashPassword(model.Password, model.Salt);

            ModelContext.Users.Add(model);
            ModelContext.SaveChanges();

            return model;
        }

        private string HashPassword(string password, string salt)
        {
            return Crypter.Blowfish.Crypt(Encoding.ASCII.GetBytes(password), salt);
        }

        public User SignIn(User model)
        {
            var dbUser = ModelContext.Users.SingleOrDefault(x => x.Email == model.Email);
            if (dbUser == null) return null;

            var passwordAttempt = HashPassword(model.Password, dbUser.Salt);
            if (dbUser.Password != passwordAttempt) return null;

            return dbUser;
        }
    }
}
