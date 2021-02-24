using Core.Framework.Models;
using Core.Models;

namespace Core.Logic.Interfaces
{
    public interface IUserLogic
    {
        User Create(User model);
        Response<User> SignIn(User model);
    }
}
