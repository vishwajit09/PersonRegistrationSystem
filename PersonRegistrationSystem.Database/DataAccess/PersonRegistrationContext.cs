using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace PersonRegistrationSystem.Database;

public class PersonRegistrationContext : DbContext
{
    public PersonRegistrationContext()
    {
    }

    public PersonRegistrationContext(DbContextOptions<PersonRegistrationContext> options)
    : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<PersonInformation> PersonInformations { get; set; }
    public DbSet<PlaceOfResidence> PlaceOfResidences { get; set; }



}

