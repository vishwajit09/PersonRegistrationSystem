using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace PersonRegistrationSystem.BussinessLogic;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IPersonInformationService, PersonInformationService>();
        services.AddScoped<IPlaceOfResidenceService, PlaceOfResidenceRepositoryService>();
        services.AddScoped<IUserService, UserService>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddTransient<IJwtService, JwtService>();
        return services;
    }
}
