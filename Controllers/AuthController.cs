using Microsoft.AspNetCore.Mvc;
using UserOrderSystem.Services;

namespace UserOrderSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AuthService authService) : ControllerBase
{
    [HttpGet("is-registered")]
    public async Task<IActionResult> IsEmailRegistered([FromQuery] string email)
    {
        var result = await authService.IsEmailRegisteredAsync(email);
        return Ok(result);
    }

    [HttpPost("validate-password")]
    public IActionResult ValidatePassword([FromBody] string password)
    {
        var result = authService.IsValidPassword(password);
        return Ok(result);
    }
}
