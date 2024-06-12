using AutoFixture;
using AutoFixture.Xunit2;

namespace PersonRegistrationSystem.Tests.Database
{
    public class PersonalInformationDataAttribute : AutoDataAttribute
    {
        public PersonalInformationDataAttribute() : base(() =>
        {
            var fixture = new Fixture();
            fixture.Customizations.Add(new PersonalInformationSpecimenBuilder());
            return fixture;
        })
        { }
    }
}


