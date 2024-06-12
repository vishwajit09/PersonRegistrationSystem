using PersonRegistrationSystem.Database;

namespace PersonRegistrationSystem.BussinessLogic;

public interface IPersonInformationService
{
    Task<PersonInformation> GetPersonByIdAsync(int id);
    Task AddPersonAsync(PersonInformation person);
    Task UpdatePersonAsync(int userId, PersonInformation person);
    Task<IEnumerable<PersonInformation>> GetAllPersonInformationAsync(int userId);

}
