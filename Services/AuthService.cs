namespace UserOrderSystem.Services;

public class AuthService
{
    private readonly List<string> _registeredEmails = ["test@example.com"];

    public Task<bool> IsEmailRegisteredAsync(string email) =>
        Task.FromResult(_registeredEmails.Contains(email));

    public bool IsValidPassword(string password)
    {
        return password.Length >= 8 &&
               password.Any(char.IsUpper) &&
               password.Any(char.IsDigit);
    }
}
