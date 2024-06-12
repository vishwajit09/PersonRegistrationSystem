using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using PersonRegistrationSystem.BussinessLogic;
using PersonRegistrationSystem.Database;


namespace PersonRegistrationSystem.Tests.Database;

public class PersonInformationServiceTests
{
    private readonly Mock<IPersonInformationRepository> _mockRepository;
    private readonly Mock<ILogger<PersonInformationService>> _mockLogger;
    private readonly Mock<IMapper> _mockMapper;
    private readonly PersonInformationService _service;

    public PersonInformationServiceTests()
    {
        _mockRepository = new Mock<IPersonInformationRepository>();
        _mockLogger = new Mock<ILogger<PersonInformationService>>();
        _mockMapper = new Mock<IMapper>();

        _service = new PersonInformationService(_mockRepository.Object, _mockLogger.Object, _mockMapper.Object);
    }

    [Theory, PersonalInformationData]
    public async Task GetPersonByIdAsync_ShouldReturnPerson_WhenPersonExists(PersonInformation person)
    {
        // Arrange
        var personId = 1;
        _mockRepository.Setup(repo => repo.GetByIdAsync(personId)).ReturnsAsync(person);

        // Act
        var result = await _service.GetPersonByIdAsync(personId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(personId, result.Id);
    }

    [Theory, PersonalInformationData]
    public async Task AddPersonAsync_ShouldThrowException_WhenEmailExists(PersonInformation person)
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetByEmailAsync(person.Email, person.UserId)).Returns(true);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.AddPersonAsync(person));
        Assert.Equal("Person with this email already exists.", exception.Message);
    }


    [Theory, PersonalInformationData]
    public async Task AddPersonAsync_ShouldThrowException_WhenPersonalCodeExists(PersonInformation person)
    {
        // Arrange      
        _mockRepository.Setup(repo => repo.GetByPersoanlCodeAsync(person.PersonalCode, person.UserId)).Returns(true);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _service.AddPersonAsync(person));
        Assert.Equal("Person with this personal code already exists.", exception.Message);
    }

    [Theory, PersonalInformationData]
    public async Task AddPersonAsync_ShouldAddPerson_WhenValid(PersonInformation person)
    {
        // Arrange        
        _mockRepository.Setup(repo => repo.GetByEmailAsync(person.Email, person.UserId)).Returns(false);
        _mockRepository.Setup(repo => repo.GetByPersoanlCodeAsync(person.PersonalCode, person.UserId)).Returns(false);

        // Act
        await _service.AddPersonAsync(person);

        // Assert
        _mockRepository.Verify(repo => repo.AddAsync(person), Times.Once);

    }

    [Theory, PersonalInformationData]
    public async Task UpdatePersonAsync_ShouldThrowException_WhenUserIdMismatch()
    {
        // Arrange
        var userId = 1;
        var person = new PersonInformation { Id = 1, UserId = 2 };
        _mockRepository.Setup(repo => repo.GetByIdAsync(person.Id)).ReturnsAsync(person);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _service.UpdatePersonAsync(userId, person));
        Assert.Equal("Cannot update another user's information.", exception.Message);
    }

    [Theory, PersonalInformationData]
    public async Task UpdatePersonAsync_ShouldUpdatePerson_WhenValid(PersonInformation person)
    {
        // Arrange
        var userId = 1;
        var existingPerson = new PersonInformation { Id = 1, UserId = 1, Name = "John Doe" };

        _mockRepository.Setup(repo => repo.GetByIdAsync(person.Id)).ReturnsAsync(existingPerson);

        // Act
        await _service.UpdatePersonAsync(userId, person);

        // Assert
        _mockMapper.Verify(mapper => mapper.Map(person, existingPerson), Times.Once);
        _mockRepository.Verify(repo => repo.UpdateAsync(existingPerson), Times.Once);

    }

    [Theory, PersonalInformationData]
    public async Task GetAllPersonInformationAsync_ShouldReturnPersonList_WhenCalled(List<PersonInformation> personInformation)
    {
        // Arrange
        var userId = 1;
        _mockRepository.Setup(repo => repo.GetAllAsync(userId)).ReturnsAsync(personInformation);

        // Act
        var result = await _service.GetAllPersonInformationAsync(userId);

        // Assert
        Assert.NotNull(result);
    }
}