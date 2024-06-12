using Microsoft.EntityFrameworkCore;

namespace PersonRegistrationSystem.Database;

public class PlaceOfResidenceRepository : IPlaceOfResidenceRepository
{
    private readonly PersonRegistrationContext _residence;

    public PlaceOfResidenceRepository(PersonRegistrationContext residence)
    {
        _residence = residence;
    }

    public async Task<IEnumerable<PlaceOfResidence>> GetAllAsync()
    {
        return await _residence.PlaceOfResidences.ToListAsync();
    }

    public async Task<PlaceOfResidence> GetByIdAsync(int id)
    {
        return await _residence.PlaceOfResidences.FindAsync(id);
    }

    public async Task AddAsync(PlaceOfResidence entity)
    {
        await _residence.PlaceOfResidences.AddAsync(entity);
        await _residence.SaveChangesAsync();
    }

    public async Task UpdateAsync(PlaceOfResidence entity)
    {
        _residence.PlaceOfResidences.Update(entity);
        await _residence.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var placeOfResidence = await _residence.PlaceOfResidences.FindAsync(id);
        if (placeOfResidence != null)
        {
            _residence.PlaceOfResidences.Remove(placeOfResidence);
            await _residence.SaveChangesAsync();
        }
    }
}
