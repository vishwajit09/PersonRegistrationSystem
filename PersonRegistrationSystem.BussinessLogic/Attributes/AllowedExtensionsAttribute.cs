using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PersonRegistrationSystem.BussinessLogic;

public class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _extensions;
    public AllowedExtensionsAttribute(string[] extensions)
    {
        _extensions = extensions;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            if (extension == null || !_extensions.Contains(extension.ToLower()))
            {
                return new ValidationResult($"Invalid file type. Only the following types are allowed: {string.Join(", ", _extensions)}");
            }
        }
        return ValidationResult.Success;
    }
}
