using AutoMapper;
using Microsoft.Extensions.Logging;
using PersonRegistrationSystem.Database;

namespace PersonRegistrationSystem.BussinessLogic;

public class PersonInformationService : IPersonInformationService
{
    private readonly IPersonInformationRepository _personInformationRepository;
    private readonly ILogger<PersonInformationService> _logger;

    private readonly IMapper _mapper;

    public PersonInformationService(IPersonInformationRepository personInformationRepository, ILogger<PersonInformationService> logger, IMapper mapper)
    {
        _personInformationRepository = personInformationRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<PersonInformation> GetPersonByIdAsync(int id)
    {
        return await _personInformationRepository.GetByIdAsync(id);
    }

    public async Task AddPersonAsync(PersonInformation person)
    {
        var emailExist = _personInformationRepository.GetByEmailAsync(person.Email, person.UserId);

        if (emailExist)
        {
            _logger.LogInformation($"Person with email {person.Email} already exists.");
            throw new ArgumentException("Person with this email already exists.");
        }
        var personalCodeExist = _personInformationRepository.GetByPersoanlCodeAsync(person.PersonalCode, person.UserId);


        if (personalCodeExist)
        {
            _logger.LogInformation($"Person with personal code {person.PersonalCode} already exists.");
            throw new ArgumentException("Person with this personal code already exists.");
        }

        await _personInformationRepository.AddAsync(person);
        _logger.LogInformation($"Person with name {person.Name} added.");
    }

    public async Task UpdatePersonAsync(int userId, PersonInformation person)
    {
        var existingPerson = await _personInformationRepository.GetByIdAsync(person.Id);
        if (existingPerson.UserId != userId)
        {
            _logger.LogInformation($"User with ID {userId} attempted to update another user's information.");
            throw new UnauthorizedAccessException("Cannot update another user's information.");
        }

        _mapper.Map(person, existingPerson);
        await _personInformationRepository.UpdateAsync(existingPerson);
        _logger.LogInformation($"Person with ID {person.Id} updated by user with ID {userId}.");
    }
    public async Task<IEnumerable<PersonInformation>> GetAllPersonInformationAsync(int userId)
    {
        return await _personInformationRepository.GetAllAsync(userId);
    }



}
