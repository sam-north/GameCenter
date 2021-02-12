using Core.DataAccess;
using Core.Logic.Interfaces;
using Core.Models;
using CryptSharp;
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
            var salt = CryptSharp.BlowfishCrypter.Blowfish.GenerateSalt();
            model.Password = Crypter.Blowfish.Crypt(Encoding.ASCII.GetBytes(model.Password), salt);

            ModelContext.Users.Add(model);
            ModelContext.SaveChanges();

            return model;
        }
    }
}
