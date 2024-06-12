using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using PersonRegistrationSystem.Database;
using System.Security.Cryptography;

namespace PersonRegistrationSystem.BussinessLogic;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly ILogger<UserService> _logger;


    public UserService(IUserRepository repository, ILogger<UserService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ResponseDto> Login(string username, string password)
    {
        var user = await _repository.GetByNameAsync(username);

        if (user is null || !VerifyPasswordHash(password, user.Password, user.PasswordSalt))
        {
            _logger.LogInformation($"Username or password does not match{username}");
            return new ResponseDto(false, "Username or password does not match", "");

        }

        return new ResponseDto(true, user);
    }

    public async Task<ResponseDto> Signup(string username, string password)
    {
        var user = await _repository.GetByNameAsync(username);
        if (user is not null)
            return new ResponseDto(false, "UserName already exists", "");

        user = CreateUser(username, password, "User");
        await _repository.AddAsync(user);
        return new ResponseDto(true, "User Created !!", "");
    }

    private User CreateUser(string username, string password, string role)
    {
        CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
        var user = new User
        {
            Username = username,
            Password = passwordHash,
            PasswordSalt = passwordSalt,
            Role = role
        };

        return user;
    }

    public async Task<ResponseDto> DeleteUsername(string username)
    {
        var user = await _repository.GetByNameAsync(username);
        if (user is not null)
        {
            await _repository.DeleteUserByNameAsync(username);
            return new ResponseDto(true, "User deleted", "");
        }
        return new ResponseDto(false, "User does not exist", "");
    }

    public async Task<ResponseDto> GetUserByName(string username)
    {
        var user = await _repository.GetByNameAsync(username);
        if (user is not null)
        {
            return new ResponseDto(true, user);
        }
#pragma warning disable CS8604 // Possible null reference argument.
        return new ResponseDto(false, user); ;
#pragma warning restore CS8604 // Possible null reference argument.
    }

    public async Task<ResponseDto> SignupAdmin(string username, string password)
    {
        var user = await _repository.GetByNameAsync(username); ;
        if (user is not null)
            return new ResponseDto(false, "User already exists", user.Role);

        user = CreateUser(username, password, "Admin");
        await _repository.UpdateAsync(user);
        return new ResponseDto(true);
    }

    public async Task<ResponseDto> UpdateUserRole(string username, string roleName)
    {
        var user = await _repository.GetByNameAsync(username); ;
        if (user is null)
        {
            _logger.LogInformation($"Username does not exist {username} role  : {user.Role}");
            return new ResponseDto(false, "User does not exist", user.Role);
        }
        if (user.Role != roleName)
        {
            user.Role = roleName;
            await _repository.UpdateAsync(user);
            return new ResponseDto(true);
        }
        else
        {
            return new ResponseDto(false, "Role already exist", user.Role);
        }

    }




    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

        return computedHash.SequenceEqual(passwordHash);
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        return _repository.GetAllAsync();
          

    }
}
