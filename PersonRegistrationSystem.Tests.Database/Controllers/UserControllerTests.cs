using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PersonRegistrationSystem.BussinessLogic;
using PersonRegistrationSystem.Database;

namespace PersonRegistrationSystem.Tests.Database;

public class UserControllerTests
{
    private readonly UserController _controller;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<ILogger<UserController>> _loggerMock;
    private readonly Mock<IJwtService> _jwtServiceMock;
    private readonly Mock<IMapper> _mapperMock;

    public UserControllerTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _loggerMock = new Mock<ILogger<UserController>>();
        _jwtServiceMock = new Mock<IJwtService>();
        _mapperMock = new Mock<IMapper>();

        _controller = new UserController(
            _userServiceMock.Object,
            _loggerMock.Object,
            _jwtServiceMock.Object,
            _mapperMock.Object
        );
    }


    [Fact]
    public async Task Signup_ReturnsOkResult_WhenRequestIsValid()
    {

        // Arrange
        var request = new UserRequest { Username = "testuser", Password = "password123" };
        var response = new ResponseDto { IsSuccess = true, Message = "User created successfully" };
        _userServiceMock.Setup(s => s.Signup(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(response);

        // Act
        var result = await _controller.Signup(request);

        // Assert
        Assert.NotNull(result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task Signup_ReturnsBadRequest_WhenSignupFails()
    {
        // Arrange
        var request = new UserRequest { Username = "testuser", Password = "password123" };
        var response = new ResponseDto { IsSuccess = false, Message = "Signup failed" };
        _userServiceMock.Setup(s => s.Signup(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(response);

        // Act
        var result = await _controller.Signup(request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsOkResult_WhenDeleteIsSuccessful()
    {
        // Arrange
        var username = "testuser";
        var response = new ResponseDto { IsSuccess = true, Message = "User deleted successfully" };
        _userServiceMock.Setup(s => s.DeleteUsername(It.IsAny<string>())).ReturnsAsync(response);

        // Act
        var result = await _controller.Delete(username);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsBadRequest_WhenDeleteFails()
    {
        // Arrange
        var username = "testuser";
        var response = new ResponseDto { IsSuccess = false, Message = "Delete failed" };
        _userServiceMock.Setup(s => s.DeleteUsername(It.IsAny<string>())).ReturnsAsync(response);

        // Act
        var result = await _controller.Delete(username);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }
    [Fact]
    public async Task GetUser_ReturnsOkResult_WhenUserExists()
    {
        // Arrange
        var username = "testuser";
        var response = new ResponseDto { IsSuccess = true, Message = "User found" };
        _userServiceMock.Setup(s => s.GetUserByName(It.IsAny<string>())).ReturnsAsync(response);

        // Act
        var result = await _controller.GetUser(username);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task GetUser_ReturnsBadRequest_WhenUserNotFound()
    {
        // Arrange
        var username = "testuser";
        var response = new ResponseDto { IsSuccess = false, Message = "User not found" };
        _userServiceMock.Setup(s => s.GetUserByName(It.IsAny<string>())).ReturnsAsync(response);

        // Act
        var result = await _controller.GetUser(username);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task GetAllUser_ReturnsOkResult_WhenUsersExist()
    {
        // Arrange
        var users = new List<User> { new User { Username = "testuser" } };
        _userServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(users);

        // Act
        var result = await _controller.GetAllUser();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task GetAllUser_ReturnsBadRequest_WhenNoUsersFound()
    {
        // Arrange
        List<User> users = null;
        _userServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(users);

        // Act
        var result = await _controller.GetAllUser();

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }

}

