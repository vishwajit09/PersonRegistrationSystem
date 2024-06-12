using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace PersonRegistrationSystem.BussinessLogic;

public class UniqueEmailAttribute : ValidationAttribute
{


    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var email = value as string;
        if (string.IsNullOrEmpty(email))
        {
            return new ValidationResult("Email is required.");
        }

        try
        {
            var mailAddress = new MailAddress(email);
        }
        catch (FormatException)
        {
            return new ValidationResult("Email format is not valid.");
        }

        // Domain verification step
        var domain = email.Split('@')[1];
        var allowedDomains = new[] { "gmail.com", "yahoo.com", "hotmail.com" };

        if (!allowedDomains.Contains(domain))
        {
            return new ValidationResult("Email domain is not valid.");
        }

        return ValidationResult.Success;

    }

}
