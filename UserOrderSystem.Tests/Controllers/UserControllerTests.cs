using Microsoft.AspNetCore.Mvc;
using Moq;
using UserOrderSystem.Controllers;
using UserOrderSystem.Models;
using UserOrderSystem.Services;
using Xunit;

namespace UserOrderSystem.Tests.Controllers;

public class UserControllerTests
{
    [Fact]
    public void GetUserById_ReturnsOk_WhenUserExists()
    {
        // Arrange
        var user = new User { Id = 1, Email = "test@example.com", Password = "pass" };
        var mockService = new Mock<UserService>(null!);
        mockService.Setup(s => s.GetUserById(1)).Returns(user);
        var controller = new UserController(mockService.Object);

        // Act
        var result = controller.GetUserById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedUser = Assert.IsType<User>(okResult.Value);
        Assert.Equal(user.Id, returnedUser.Id);
        Assert.Equal(user.Email, returnedUser.Email);
    }

    [Fact]
    public void GetUserById_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var mockService = new Mock<UserService>(null!);
        mockService.Setup(s => s.GetUserById(2)).Returns((User?)null);
        var controller = new UserController(mockService.Object);

        // Act
        var result = controller.GetUserById(2);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void GetAllUsers_ReturnsOkWithUserList()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, Email = "a@b.com", Password = "pass" },
            new User { Id = 2, Email = "b@c.com", Password = "pass2" }
        };
        var mockService = new Mock<UserService>(null!);
        mockService.Setup(s => s.GetAllUsers()).Returns(users);
        var controller = new UserController(mockService.Object);

        // Act
        var result = controller.GetAllUsers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedUsers = Assert.IsType<List<User>>(okResult.Value);
        Assert.Equal(2, returnedUsers.Count);
    }
}
