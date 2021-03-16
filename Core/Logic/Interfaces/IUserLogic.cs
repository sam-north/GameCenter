using Core.Framework.Models;
using Core.Models;

namespace Core.Logic.Interfaces
{
    public interface IUserLogic
    {
        User Get(string email);
        User Create(User model);
        IResponse<User> SignIn(User model);
    }
}
