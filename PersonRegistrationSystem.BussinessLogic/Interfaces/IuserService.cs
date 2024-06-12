using PersonRegistrationSystem.Database;

namespace PersonRegistrationSystem.BussinessLogic;

public interface IUserService
{
    public Task<ResponseDto> Login(string username, string password);


    public Task<ResponseDto> Signup(string username, string password);

    public Task<ResponseDto> DeleteUsername(string username);

    public Task<ResponseDto> SignupAdmin(string username, string password)
   ;
    public Task<ResponseDto> UpdateUserRole(string username, string roleName)
    ;

    public Task<ResponseDto> GetUserByName(string username);

    public Task<IEnumerable<User>> GetAllAsync();


}
