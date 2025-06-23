using Microsoft.AspNetCore.Mvc;
using UserOrderSystem.Services;

namespace UserOrderSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(UserService userService) : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        var user = userService.GetUserById(id);
        return user is null ? NotFound() : Ok(user);
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        return Ok(userService.GetAllUsers());
    }
}
