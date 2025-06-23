using UserOrderSystem.Interfaces;

namespace UserOrderSystem.Services;

public class AuthService(IUserRepository userRepository)
{
    private readonly IUserRepository _userRepository = userRepository;

    public Task<bool> IsEmailRegisteredAsync(string email)
    {
        var users = _userRepository.GetAllUsers();
        return Task.FromResult(users.Any(u => u.Email == email));
    }

    public bool IsValidPassword(string password)
    {
        if (string.IsNullOrEmpty(password) || password.Length < 8)
            return false;
        if (!password.Any(char.IsUpper))
            return false;
        if (!password.Any(char.IsDigit))
            return false;
        return true;
    }
}
