using Moq;
using UserOrderSystem.Interfaces;
using UserOrderSystem.Models;
using UserOrderSystem.Services;
using Xunit;

namespace UserOrderSystem.Tests.ServicesTests;

public class AuthServiceTests
{
    [Fact]
    public async Task IsEmailRegisteredAsync_ReturnsTrue_WhenEmailExists()
    {
        // Arrange
        var users = new List<User> { new() { Id = 1, Email = "sagunchauhan@yopmail.com", Password = "Sagun1234" } };
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(r => r.GetAllUsers()).Returns(users);

        var authService = new AuthService(mockRepo.Object);

        // Act
        var result = await authService.IsEmailRegisteredAsync("sagunchauhan@yopmail.com");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task IsEmailRegisteredAsync_ReturnsFalse_WhenEmailDoesNotExist()
    {
        // Arrange
        var users = new List<User> { new() { Id = 1, Email = "test@domain.com", Password = "pass" } };
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(r => r.GetAllUsers()).Returns(users);

        var authService = new AuthService(mockRepo.Object);

        // Act
        var result = await authService.IsEmailRegisteredAsync("notfound@domain.com");

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("Password1", true)]      // Valid: 9 chars, uppercase, number
    [InlineData("password1", false)]     // No uppercase
    [InlineData("Password", false)]      // No number
    [InlineData("Pass1", false)]         // Too short
    [InlineData("PASSWORD1", true)]      // Valid: all uppercase, has number
    [InlineData("12345678", false)]      // No uppercase
    [InlineData("Passw0rd", true)]       // Valid: 8 chars, uppercase, number
    [InlineData("", false)]              // Empty
    [InlineData(null, false)]            // Null
    public void IsValidPassword_WorksAsExpected(string password, bool expected)
    {
        var mockRepo = new Mock<IUserRepository>();
        var authService = new AuthService(mockRepo.Object);

        var result = authService.IsValidPassword(password);

        Assert.Equal(expected, result);
    }
}