using Microsoft.EntityFrameworkCore;
using PersonRegistrationSystem.Database;

namespace PersonRegistrationSystem.Tests.Database;

public class UserRepositorieyTests
{
    [Theory, UserData]
    public async Task GetByNameAsync_ShouldReturnUserWithGivenName(User user)
    {
        // Arrange
        var options = new DbContextOptionsBuilder<PersonRegistrationContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var testUserName = "string";

        // Act
        var context = new PersonRegistrationContext(options);
        var resultRepository = new UserRepository(context);

        await resultRepository.AddAsync(user);

        var result = await resultRepository.GetByNameAsync(testUserName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(testUserName, result.Username);

    }

    [Theory, UserData]
    public async Task AddAsync_ShouldAddUserToDatabase(User user)
    {
        // Arrange
        var options = new DbContextOptionsBuilder<PersonRegistrationContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        // Act
        var context = new PersonRegistrationContext(options);

        var userRepository = new UserRepository(context);
        await userRepository.AddAsync(user);

        // Assert
        var result = await context.Users.FindAsync(user.Id);
        Assert.NotNull(result);
        Assert.Equal(user.Username, result.Username);

    }

    [Theory, UserData]
    public async Task UpdateAsync_ShouldUpdateUserInDatabase(User user)
    {
        // Arrange
        var options = new DbContextOptionsBuilder<PersonRegistrationContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;



        var context = new PersonRegistrationContext(options);

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        // Act
        var userRepository = new UserRepository(context);
        user.Username = "updatedUser";
        await userRepository.UpdateAsync(user);

        // Assert      
        var result = await context.Users.FindAsync(user.Id);
        Assert.NotNull(result);
        Assert.Equal("updatedUser", result.Username);

    }


}

