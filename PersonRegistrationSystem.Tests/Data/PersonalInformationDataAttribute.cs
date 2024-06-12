namespace PersonRegistrationSystem.Tests;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Microsoft.Extensions.Logging;
using Moq;

public class PersonalInformationDataAttribute : AutoDataAttribute

{
    // public PersonalInformationDataAttribute() : base(() =>
    //        {
    //            var fixture = new Fixture();
    //            fixture.Customizations.Add(new PersonalInformationPrincipalBuilder());
    //            fixture.Customize<Mock<ILogger<PersonalInformationController>>>(c => c.FromFactory(() => new Mock<ILogger<PersonalInformationController>>()));
    //            return fixture;
    //        })
    // { }

    public PersonalInformationDataAttribute() : base(() => new Fixture().Customize(new PersonalInformationustomization()))
    {
    }
}
public class PersonalInformationustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize(new AutoMoqCustomization());
        fixture.Customize<Mock<ILogger<PersonalInformationController>>>(c => c.FromFactory(() => new Mock<ILogger<PersonalInformationController>>()));
    }
}


