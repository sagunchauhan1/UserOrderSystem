using UserOrderSystem.Interfaces;
using UserOrderSystem.Models;
using UserOrderSystem.Services;
using Xunit;
using Moq;

namespace UserOrderSystem.Tests.ServicesTests;

public class UserRepositoryTests
{
    [Fact]
    public void GetUserById_ReturnsExpectedUser()
    {
        // Arrange
        var expectedUser = new User
        {
            Id = 1,
            Email = "sagunchauhan@harakirimail.com",
            Password = "Sagun1"
        };

        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(repo => repo.GetUserById(1)).Returns(expectedUser);

        var userService = new UserService(mockRepo.Object);

        // Act
        var result = userService.GetUserById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedUser.Id, result?.Id);
        Assert.Equal(expectedUser.Email, result?.Email);
        Assert.Equal(expectedUser.Password, result?.Password);
    }
}