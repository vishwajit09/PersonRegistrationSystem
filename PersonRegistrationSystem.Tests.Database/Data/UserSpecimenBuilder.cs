using AutoFixture.Kernel;
using PersonRegistrationSystem.BussinessLogic;
using PersonRegistrationSystem.Database;

namespace PersonRegistrationSystem.Tests.Database;

public class UserSpecimenBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        if (request is Type t && t == typeof(User))
        {
            return new User
            {
                Id = 1,
                Password = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                PasswordSalt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                Role = "User",
                Username = "string"
            };
        }

        if (request is Type t12 && t12 == typeof(UserRequest))
        {
            return new UserRequest
            {

                Username = "string",
                Password = "Password"


            };


        }


        if (request is Type t5 && t5 == typeof(string))
        {
            return "Token";
        }

        return new NoSpecimen();
    }
}

