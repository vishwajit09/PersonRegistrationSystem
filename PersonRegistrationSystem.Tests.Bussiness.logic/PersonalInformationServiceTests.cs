using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using PersonRegistrationSystem.BussinessLogic;
using PersonRegistrationSystem.Database;

namespace PersonRegistrationSystem.Tests.Bussiness.logic;

public class PersonalInformationServiceTests
{
   private readonly Mock<IPersonInformationRepository> _personInformationRepositoryMock;
    private readonly Mock<ILogger<PersonInformationService>> _loggerMock;
    private readonly IMapper _mapper;
    private readonly PersonInformationService _personInformationService;

    public PersonalInformationServiceTests()
    {
        _personInformationRepositoryMock = new Mock<IPersonInformationRepository>();
        _loggerMock = new Mock<ILogger<PersonInformationService>>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<PersonInformation, PersonInformation>();
        });

        _mapper = config.CreateMapper();
        _personInformationService = new PersonInformationService(
            _personInformationRepositoryMock.Object, 
            _loggerMock.Object, 
            _mapper
        );
    }

    [Fact]
    public async Task GetPersonByIdAsync_ReturnsPerson()
    {
        // Arrange
        var personId = 1;
        var expectedPerson = new PersonInformation { Id = personId };
        _personInformationRepositoryMock
            .Setup(repo => repo.GetByIdAsync(personId))
            .ReturnsAsync(expectedPerson);

        // Act
        var result = await _personInformationService.GetPersonByIdAsync(personId);

        // Assert
        Assert.Equal(expectedPerson, result);
    }

    [Fact]
    public async Task AddPersonAsync_AddsPerson()
    {
        // Arrange
        var person = new PersonInformation { Name = "John Doe" };

        // Act
        await _personInformationService.AddPersonAsync(person);

        // Assert
        _personInformationRepositoryMock.Verify(repo => repo.AddAsync(person), Times.Once);
        // _loggerMock.Verify(logger => logger.LogInformation(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task UpdatePersonAsync_UpdatesPerson()
    {
        // Arrange
        var userId = 1;
        var person = new PersonInformation { Id = 1, UserId = userId, Name = "Jane Doe" };
        var existingPerson = new PersonInformation { Id = 1, UserId = userId, Name = "John Doe" };

        _personInformationRepositoryMock
            .Setup(repo => repo.GetByIdAsync(person.Id))
            .ReturnsAsync(existingPerson);

        // Act
        await _personInformationService.UpdatePersonAsync(userId, person);

        // Assert
        _personInformationRepositoryMock.Verify(repo => repo.UpdateAsync(existingPerson), Times.Once);
        // _loggerMock.Verify(logger => logger.LogInformation(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task UpdatePersonAsync_UnauthorizedAccess_ThrowsException()
    {
        // Arrange
        var userId = 1;
        var person = new PersonInformation { Id = 1, UserId = 2, Name = "Jane Doe" };
        var existingPerson = new PersonInformation { Id = 1, UserId = 2, Name = "John Doe" };

        _personInformationRepositoryMock
            .Setup(repo => repo.GetByIdAsync(person.Id))
            .ReturnsAsync(existingPerson);

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _personInformationService.UpdatePersonAsync(userId, person));
        _loggerMock.Verify(logger => logger.LogInformation(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task GetAllPersonInformationAsync_ReturnsAllPersons()
    {
        // Arrange
        var userId = 1;
        var expectedPersons = new List<PersonInformation>
        {
            new PersonInformation { Id = 1, UserId = userId, Name = "John Doe" },
            new PersonInformation { Id = 2, UserId = userId, Name = "Jane Doe" }
        };

        _personInformationRepositoryMock
            .Setup(repo => repo.GetAllAsync(userId))
            .ReturnsAsync(expectedPersons);

        // Act
        var result = await _personInformationService.GetAllPersonInformationAsync(userId);

        // Assert
        Assert.Equal(expectedPersons, result);
    }

    [Fact]
    public void GetByEmailAsync_ReturnsBoolean()
    {
        // Arrange
        var email = "test@example.com";
        var expectedResult = true;

        _personInformationRepositoryMock
            .Setup(repo => repo.GetByEmailAsync(email))
            .Returns(expectedResult);

        // Act
        var result = _personInformationService.GetByEmailAsync(email);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void GetByPersoanlCodeAsync_ReturnsBoolean()
    {
        // Arrange
        var personalCode = "123456";
        var expectedResult = true;

        _personInformationRepositoryMock
            .Setup(repo => repo.GetByPersoanlCodeAsync(personalCode))
            .Returns(expectedResult);

        // Act
        var result = _personInformationService.GetByPersoanlCodeAsync(personalCode);

        // Assert
        Assert.Equal(expectedResult, result);
    }
}