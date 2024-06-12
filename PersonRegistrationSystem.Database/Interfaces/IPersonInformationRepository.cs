namespace PersonRegistrationSystem.Database;

public interface IPersonInformationRepository
{
    Task AddAsync(PersonInformation entity);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<PersonInformation>> GetAllAsync(int userId);
    Task<PersonInformation> GetByIdAsync(int id);
    Task UpdateAsync(PersonInformation entity);

    bool GetByEmailAsync(string id, int userId);

    bool GetByPersoanlCodeAsync(string id, int userId);
}
