using System;
using AutoFixture;
using AutoFixture.Kernel;
using PersonRegistrationSystem.BussinessLogic;
using PersonRegistrationSystem.Database;


namespace PersonRegistrationSystem.Tests.Database;


public class PersonalInformationSpecimenBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        if (request is Type t && t == typeof(PersonInformation))
        {
            return new PersonInformation
            {
                Id = 1,
                Name = "Test",
                LastName = "LastName",
                Gender = "Male",
                Birthday = new DateTime(),
                PersonalCode = "12345678901",
                TelephoneNumber = "+37068653465",
                Email = "email@example.com",
                ProfilePhoto = new byte[] { 0, 1, 2, 3, 4, 5 },
                PlaceOfResidence = null,
                UserId = 1


            };
        }

        if (request is Type t1 && t1 == typeof(PersonInformationResponse))
        {
            return new PersonInformationResponse
            {
                Id = 1,
                Name = "Test",
                LastName = "LastName",
                Gender = "Male",
                Birthday = new DateTime(),
                PersonalCode = "12345678901",
                TelephoneNumber = "+37068653465",
                Email = "email@example.com",
                PlaceOfResidence = null


            };
        }

        if (request is Type t12 && t12 == typeof(PersonInformationRequest))
        {
            return new PersonInformationRequest
            {

                Name = "Test",
                LastName = "LastName",
                Gender = "Male",
                Birthday = new DateTime(),
                PersonalCode = "12345678901",
                TelephoneNumber = "+37068653465",
                Email = "email@example.com",
                PlaceOfResidence = null



            };


        }

        if (request is Type t14 && t14 == typeof(PersonInformationUpdate))
        {
            return new PersonInformationUpdate
            {

                Name = "Test",
                LastName = "LastName",
                Gender = "Male",
                Birthday = new DateTime(),
                PersonalCode = "12345678901",
                TelephoneNumber = "+37068653465",
                Email = "email@example.com",
                PlaceOfResidence = null



            };
        }

        if (request is Type t15 && t15 == typeof(List<PersonInformation>))
        {
            return new List<PersonInformation>
            {
new PersonInformation {
                Name = "Test",
                LastName = "LastName",
                Gender = "Male",
                Birthday = new DateTime(),
                PersonalCode = "12345678901",
                TelephoneNumber = "+37068653465",
                Email = "email@example.com",
                PlaceOfResidence = null}



            };



        }
        return new NoSpecimen();
    }
}
