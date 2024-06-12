using AutoFixture;
using AutoFixture.Xunit2;

namespace PersonRegistrationSystem.Tests.Database;

public class UserDataAttribute : AutoDataAttribute
{
    public UserDataAttribute() : base(() =>
    {
        var fixture = new Fixture();
        fixture.Customizations.Add(new UserSpecimenBuilder());
        return fixture;
    })
    { }
}
