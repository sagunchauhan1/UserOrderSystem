using UserOrderSystem.Interfaces;
using UserOrderSystem.Models;

namespace UserOrderSystem.Services;

public class UserService(IUserRepository userService)
{
    private readonly IUserRepository _userService = userService;

    public User? GetUserById(int id) => _userService.GetUserById(id);
    public List<User> GetAllUsers() => _userService.GetAllUsers();
}