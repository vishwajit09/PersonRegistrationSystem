namespace PersonRegistrationSystem.Database;

public interface IPlaceOfResidenceRepository
{
    Task AddAsync(PlaceOfResidence entity);
    Task DeleteAsync(int id);
    Task<IEnumerable<PlaceOfResidence>> GetAllAsync();
    Task<PlaceOfResidence> GetByIdAsync(int id);
    Task UpdateAsync(PlaceOfResidence entity);
}
