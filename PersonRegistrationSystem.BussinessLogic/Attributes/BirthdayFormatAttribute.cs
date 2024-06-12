using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace PersonRegistrationSystem.BussinessLogic;

public class BirthdayFormatAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var birthday = value as string;


        if (string.IsNullOrEmpty(birthday))
        {
            return new ValidationResult("Birthday Cannot be empty");
        }

        if (!DateTime.TryParseExact(birthday, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
        {
            return new ValidationResult("Invalid birthday format. Please use the format yyyy-MM-dd.");
        }

        return ValidationResult.Success;
    }
}