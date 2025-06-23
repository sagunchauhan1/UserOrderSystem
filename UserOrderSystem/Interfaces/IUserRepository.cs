using UserOrderSystem.Models;

namespace UserOrderSystem.Interfaces;

public interface IUserRepository
{
    User? GetUserById(int id);
    List<User> GetAllUsers();
}
