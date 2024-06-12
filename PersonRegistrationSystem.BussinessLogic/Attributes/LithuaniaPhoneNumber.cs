using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PersonRegistrationSystem.BussinessLogic;


public class LithuaniaPhoneNumberAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var phoneNumber = value as string;
        if (string.IsNullOrEmpty(phoneNumber))
        {
            return new ValidationResult("Phone number is required.");
        }

        if (!Regex.IsMatch(phoneNumber, @"^\+370\d{8}$"))
        {
            return new ValidationResult("Invalid Lithuanian phone number.");
        }

        return ValidationResult.Success;
    }
}