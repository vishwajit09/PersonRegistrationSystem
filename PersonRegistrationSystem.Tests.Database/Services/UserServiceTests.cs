using Xunit;
using Moq;
using System.Threading.Tasks;
using PersonRegistrationSystem.BussinessLogic;
using PersonRegistrationSystem.Database;
using Microsoft.Extensions.Logging;
using AutoFixture.Xunit2;
using System.Security.Cryptography;
namespace PersonRegistrationSystem.Tests.Database;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _mockuserRepositoryMock;
    private readonly Mock<ILogger<UserService>> _mockloggerMock;
    private readonly UserService _service;

    public UserServiceTests()
    {
        _mockuserRepositoryMock = new Mock<IUserRepository>();
        _mockloggerMock = new Mock<ILogger<UserService>>();
        _service = new UserService(_mockuserRepositoryMock.Object, _mockloggerMock.Object);
    }

    [Theory, UserData]
    public async Task Login_ValidCredentials_ReturnsSuccessResponse(User user)
    {
        var username = "string";
        var password = "string";

        user.Password = ComputePasswordHash(password, user.PasswordSalt); // Compute the password hash for the user

        _mockuserRepositoryMock.Setup(repo => repo.GetByNameAsync(username)).ReturnsAsync(user);

        // Act
        var response = await _service.Login(username, password);

        // Assert
        byte[] ComputePasswordHash(string password, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            return hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        Assert.True(response.IsSuccess);
    }

    [Fact]
    public async Task Signup_NewUsername_ReturnsSuccessResponse()
    {
        // Arrange

        _mockuserRepositoryMock.Setup(repo => repo.GetByNameAsync("newuser")).ReturnsAsync((User)null);

        var loggerMock = new Mock<ILogger<UserService>>();

        // Act
        var response = await _service.Signup("newuser", "password");

        // Assert
        Assert.True(response.IsSuccess);
    }

    [Fact]
    public async Task DeleteUsername_ExistingUser_ReturnsSuccessResponse()
    {
        // Arrange

        _mockuserRepositoryMock.Setup(repo => repo.GetByNameAsync("existinguser")).ReturnsAsync(new User());

        // Act
        var response = await _service.DeleteUsername("existinguser");

        // Assert
        Assert.True(response.IsSuccess);
    }


    [Theory, UserData]
    public async Task GetUserByName_ExistingUser_ReturnsSuccessResponse(User user)
    {
        // Arrange
        var username = "existinguser";

        _mockuserRepositoryMock.Setup(repo => repo.GetByNameAsync(username)).ReturnsAsync(user);


        // Act
        var response = await _service.GetUserByName(username);

        // Assert
        Assert.True(response.IsSuccess);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsListOfUsers()
    {
        // Arrange
        var users = new List<User>
            {
                new User { Id = 1, Username = "user1" },
                new User { Id = 2, Username = "user2" },
                new User { Id = 3, Username = "user3" }
            };

        _mockuserRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, ((List<User>)result).Count); // Assuming you're using List<User> in your repository
    }
}
