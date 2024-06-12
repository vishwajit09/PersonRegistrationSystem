using System.ComponentModel.DataAnnotations;

namespace PersonRegistrationSystem.BussinessLogic;

public class CityAttribute : ValidationAttribute
{

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var city = value as string;
        var lithuanianCities = new[] { "Vilnius", "Kaunas", "Klaipėda", "Šiauliai", "Panevėžys" };
        if (lithuanianCities.Contains(city))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("City is not a valid Lithuanian city.");
    }

}
