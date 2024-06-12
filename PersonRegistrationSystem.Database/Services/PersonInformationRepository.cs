using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PersonRegistrationSystem.Database;

public class PersonInformationRepository : IPersonInformationRepository
{
    private readonly PersonRegistrationContext _personInformationContext;

    public PersonInformationRepository(PersonRegistrationContext personRegistrationContext)
    {
        _personInformationContext = personRegistrationContext;
    }

    public async Task<IEnumerable<PersonInformation>> GetAllAsync(int userId)
    {
        return await _personInformationContext.PersonInformations.Include(x => x.PlaceOfResidence).Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<PersonInformation> GetByIdAsync(int id)
    {
        return await _personInformationContext.PersonInformations.Include(x => x.PlaceOfResidence).SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(PersonInformation entity)
    {
        await _personInformationContext.PersonInformations.AddAsync(entity);
        await _personInformationContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(PersonInformation entity)
    {
        _personInformationContext.PersonInformations.Update(entity);
        await _personInformationContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var person = await _personInformationContext.PersonInformations.FindAsync(id);
        if (person != null)
        {
            _personInformationContext.PersonInformations.Remove(person);
            await _personInformationContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public bool GetByEmailAsync(string id, int userId)
    {
        var personInfo = _personInformationContext.PersonInformations.FirstOrDefault(x => x.Email == id && x.UserId == userId);
        if (personInfo != null)
        {
            return true;
        }
        return false;
    }

    public bool GetByPersoanlCodeAsync(string id, int userId)
    {
        var personInfo = _personInformationContext.PersonInformations.FirstOrDefault(x => x.PersonalCode == id && x.UserId == userId);
        if (personInfo != null)
        {
            return true;
        }
        return false;

    }
}
