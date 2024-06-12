using Microsoft.EntityFrameworkCore;
using PersonRegistrationSystem.Database;
using System.Threading.Tasks;
namespace PersonRegistrationSystem.Tests.Database;

public class PersonInformationRepositoryTests
{

    [Theory, PersonalInformationData]
    public async Task GetByIdAsync_ShouldReturnCorrectPersonInformation(PersonInformation personInformation)
    {
        var options = new DbContextOptionsBuilder<PersonRegistrationContext>()
            .UseInMemoryDatabase(databaseName: "Personalinfo")
            .Options;

        var context = new PersonRegistrationContext(options);


        var repository = new PersonInformationRepository(context);

        await repository.AddAsync(personInformation);

        var result = await repository.GetByIdAsync(personInformation.Id);
        Assert.Equal(personInformation, result);

    }

    [Theory, PersonalInformationData]
    public async Task UpdateAsync_ShouldUpdatePersonInformation(PersonInformation personInformation)
    {
        var options = new DbContextOptionsBuilder<PersonRegistrationContext>()
            .UseInMemoryDatabase(databaseName: "Personalinfo1")
            .Options;

        var context = new PersonRegistrationContext(options);

        var repository = new PersonInformationRepository(context);
        await repository.AddAsync(personInformation);

        personInformation.Name = "NameTest";
        await repository.UpdateAsync(personInformation);


        var updatedPersonInformation = await context.PersonInformations.FindAsync(personInformation.Id);
        Assert.Equal("NameTest", updatedPersonInformation.Name);



    }


    [Theory, PersonalInformationData]
    public async Task DeleteAsync_ShouldDeletePersonInformation(PersonInformation personInformation)
    {
        var options = new DbContextOptionsBuilder<PersonRegistrationContext>()
               .UseInMemoryDatabase(databaseName: "Personalinfo2")
               .Options;
        var context = new PersonRegistrationContext(options);

        var repository = new PersonInformationRepository(context);
        await repository.AddAsync(personInformation);
        await repository.DeleteAsync(personInformation.Id);


        var deletedPersonInformation = await context.PersonInformations.FindAsync(personInformation.Id);
        Assert.Null(deletedPersonInformation);

    }
}
