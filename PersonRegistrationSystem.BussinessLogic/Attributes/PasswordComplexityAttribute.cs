using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PersonRegistrationSystem.BussinessLogic;

public class PasswordComplexityAttribute : ValidationAttribute
{

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string password)
        {
            if (password.Length < 12)
            {
                return new ValidationResult("Password Must be 12 char long");
            }

            if (Regex.Matches(password, @"[A-Z]").Count < 2)
            {
                return new ValidationResult("Password Must have 2 Uppercase letters");
            }

            if (Regex.Matches(password, @"[a-z]").Count < 2)
            {
                return new ValidationResult("Password Must have 2 Lower letters");
            }

            if (Regex.Matches(password, @"\d").Count < 2)
            {
                return new ValidationResult("Password Must have 2 Numbers");
            }

            if (Regex.Matches(password, @"[\W]").Count < 2)
            {
                return new ValidationResult("Password Must have 2 Special Character");
            }


        }

        return ValidationResult.Success;
    }
}
