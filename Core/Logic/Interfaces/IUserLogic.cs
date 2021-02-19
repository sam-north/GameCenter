using Core.Models;

namespace Core.Logic.Interfaces
{
    public interface IUserLogic
    {
        User Create(User model);
        User SignIn(User model);
    }
}
