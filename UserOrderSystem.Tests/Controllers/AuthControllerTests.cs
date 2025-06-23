using Microsoft.AspNetCore.Mvc;
using Moq;
using UserOrderSystem.Controllers;
using UserOrderSystem.Services;
using Xunit;

namespace UserOrderSystem.Tests.Controllers;

public class AuthControllerTests
{
    [Fact]
    public async Task IsEmailRegistered_ReturnsOkWithExpectedResult()
    {
        // Arrange
        var mockAuthService = new Mock<AuthService>(null!);
        mockAuthService
            .Setup(s => s.IsEmailRegisteredAsync("test@example.com"))
            .ReturnsAsync(true);

        var controller = new AuthController(mockAuthService.Object);

        // Act
        var result = await controller.IsEmailRegistered("test@example.com");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.True((bool)okResult.Value!);
    }

    [Theory]
    [InlineData("Password1", true)]
    [InlineData("short", false)]
    public void ValidatePassword_ReturnsOkWithExpectedResult(string password, bool expected)
    {
        // Arrange
        var mockAuthService = new Mock<AuthService>(null!);
        mockAuthService
            .Setup(s => s.IsValidPassword(password))
            .Returns(expected);

        var controller = new AuthController(mockAuthService.Object);

        // Act
        var result = controller.ValidatePassword(password);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(expected, okResult.Value);
    }
}
