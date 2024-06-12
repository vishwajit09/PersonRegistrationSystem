using Microsoft.EntityFrameworkCore;

namespace PersonRegistrationSystem.Database;

public class UserRepository : IUserRepository
{
    private readonly PersonRegistrationContext _personRegistrationContext;

    public UserRepository(PersonRegistrationContext personRegistrationContext)
    {
        _personRegistrationContext = personRegistrationContext;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _personRegistrationContext.Users.Include(x => x.PersonInformation).ToListAsync();
    }
    public async Task<User> GetByIdAsync(int id)
    {
        return await _personRegistrationContext.Users
               .Include(x => x.PersonInformation).SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User> GetByNameAsync(string name)
    {
        return await _personRegistrationContext.Users
               .Include(x => x.PersonInformation).SingleOrDefaultAsync(x => x.Username == name);
    }
    public async Task AddAsync(User user)
    {
        await _personRegistrationContext.Users.AddAsync(user);
        await _personRegistrationContext.SaveChangesAsync();
    }
    public async Task UpdateAsync(User user)
    {
        _personRegistrationContext.Users.Update(user);
        await _personRegistrationContext.SaveChangesAsync();
    }
    public async Task DeleteUserbyIdAsync(int id)
    {
        var user = await _personRegistrationContext.Users.FindAsync(id);
        if (user != null)
        {
            _personRegistrationContext.Users.Remove(user);
            await _personRegistrationContext.SaveChangesAsync();
        }
    }
    public async Task DeleteUserByNameAsync(string name)
    {
        var user = await _personRegistrationContext.Users.SingleOrDefaultAsync(x => x.Username == name);
        if (user != null)
        {
            _personRegistrationContext.Users.Remove(user);
            await _personRegistrationContext.SaveChangesAsync();
        }
    }
}
