
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace PersonRegistrationSystem.Database;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, string connectionString)
    {
        //services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPersonInformationRepository, PersonInformationRepository>();
        services.AddScoped<IPlaceOfResidenceRepository, PlaceOfResidenceRepository>();
        // services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddDbContext<PersonRegistrationContext>(options => options.UseSqlServer(connectionString));

        return services;
    }
}
