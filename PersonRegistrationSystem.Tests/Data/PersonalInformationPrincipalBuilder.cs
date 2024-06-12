using System.Security.Claims;
using AutoFixture;
using AutoFixture.Kernel;
using PersonRegistrationSystem.Database;

namespace PersonRegistrationSystem.Tests;

public class PersonalInformationPrincipalBuilder : ISpecimenBuilder
{


    public object Create(object request, ISpecimenContext context)
    {
        if (request is Type type && type == typeof(ClaimsPrincipal))
        {
            var userId = context.Create<string>();
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            return new ClaimsPrincipal(identity);
        }
        if (request is Type t && t == typeof(User))
        {
            return new User
            {
                Id = 1,
                Password = [],
                PasswordSalt = [],
                Role = "User",
                Username = "Username"
            };
        }

        if (request is Type t5 && t5 == typeof(string))
        {
            return "Token";
        }
        return new NoSpecimen();


    }

}
