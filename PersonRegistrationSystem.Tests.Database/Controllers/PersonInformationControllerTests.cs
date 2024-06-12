using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PersonRegistrationSystem.BussinessLogic;
using PersonRegistrationSystem.Database;

namespace PersonRegistrationSystem.Tests.Database;

public class PersonInformationControllerTests
{
    private readonly Mock<IPersonInformationService> _personInformationServiceMock;
    private readonly Mock<IPlaceOfResidenceService> _placeOfResidenceServiceMock;
    private readonly IMapper _mapperMock;
    private readonly Mock<ILogger<PersonalInformationController>> _loggerMock;
    private readonly PersonalInformationController _controller;

    public PersonInformationControllerTests()
    {
        _personInformationServiceMock = new Mock<IPersonInformationService>();
        _placeOfResidenceServiceMock = new Mock<IPlaceOfResidenceService>();
        _mapperMock = new Mapper(new MapperConfiguration(cfg =>
           cfg.AddProfile(new MappingProfile())));
        _loggerMock = new Mock<ILogger<PersonalInformationController>>();
        _controller = new PersonalInformationController(
            _personInformationServiceMock.Object,
            _mapperMock,
            _loggerMock.Object,
            _placeOfResidenceServiceMock.Object
        );


    }

    [Theory, PersonalInformationData]
    public async Task AddPerosnalInformation_Returns_OkResultAsync(int id, PersonInformationRequest personInformation)
    {
        var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString())
            };

        var user = new ClaimsPrincipal(new ClaimsIdentity(userClaims));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        // Act
        var result = await _controller.AddPersonInformation(personInformation);

        // Assert
        Assert.IsType<OkResult>(result);

    }

    [Theory, PersonalInformationData]
    public async Task GetAllPersonalInformationAsync_ReturnsOkResult_WhenUserExists(List<PersonInformation> personInformation)
    {
        // Arrange


        var userClaims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, "1")
    };

        var user = new ClaimsPrincipal(new ClaimsIdentity(userClaims));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        _personInformationServiceMock.Setup(service => service.GetAllPersonInformationAsync(1))
            .ReturnsAsync(personInformation);

        // Act
        var result = await _controller.GetAllPersonalInformationAsync();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Theory, PersonalInformationData]
    public async Task GetAllPersonalInformationAsync_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange

        var userClaims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, "1")
    };

        var user = new ClaimsPrincipal(new ClaimsIdentity(userClaims));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        _personInformationServiceMock.Setup(service => service.GetAllPersonInformationAsync(It.IsAny<int>()))
            .ReturnsAsync((List<PersonInformation>)null);

        // Act
        var result = await _controller.GetAllPersonalInformationAsync();

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetAllPersonalInformationAsync_ReturnsBadRequest_WhenExceptionIsThrown()
    {
        // Arrange
        var id = 1;
        var userClaims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, id.ToString())
    };

        var user = new ClaimsPrincipal(new ClaimsIdentity(userClaims));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        _personInformationServiceMock.Setup(service => service.GetAllPersonInformationAsync(It.IsAny<int>()))
            .Throws(new Exception());

        // Act
        var result = await _controller.GetAllPersonalInformationAsync();

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Theory, PersonalInformationData]
    public async void PutPersonalInformationbyIdAsync_ReturnsNoContent_WhenUpdateIsSuccessful(PersonInformationUpdate personInformationUpdate)
    {
        // Arrange
        var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1")
            };

        var user = new ClaimsPrincipal(new ClaimsIdentity(userClaims));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };



        // Act
        var result = await _controller.PutPersonalInformationbyIdAsync(1, personInformationUpdate);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Theory, PersonalInformationData]
    public async void PutPersonalInformationbyIdAsync_ReturnsNotFound_WhenUserClaimNotFound(PersonInformationUpdate personalInformationData)
    {
        // Arrange
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Act
        var result = await _controller.PutPersonalInformationbyIdAsync(1, personalInformationData);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Theory, PersonalInformationData]
    public async void PutPersonalInformationbyIdAsync_ReturnsBadRequest_WhenUpdateFails(PersonInformationUpdate personalInformationData)
    {
        // Arrange
        var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1")
            };

        var user = new ClaimsPrincipal(new ClaimsIdentity(userClaims));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };


        _personInformationServiceMock.Setup(p => p.UpdatePersonAsync(It.IsAny<int>(), It.IsAny<PersonInformation>())).Throws(new Exception());

        // Act
        var result = await _controller.PutPersonalInformationbyIdAsync(1, personalInformationData);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}




