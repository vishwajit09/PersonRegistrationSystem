namespace PersonRegistrationSystem.Database;

public interface IUserRepository
{
    public  Task<IEnumerable<User>> GetAllAsync();
    public  Task<User> GetByIdAsync(int id) ;
    public  Task<User> GetByNameAsync(string  name) ;
    public  Task AddAsync(User user) ;
    public  Task UpdateAsync(User user) ;
    public  Task DeleteUserbyIdAsync(int id);
    public  Task DeleteUserByNameAsync(string name);

}
