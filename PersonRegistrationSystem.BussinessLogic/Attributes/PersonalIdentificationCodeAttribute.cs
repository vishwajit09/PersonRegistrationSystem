using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using PersonRegistrationSystem.Database;

namespace PersonRegistrationSystem.BussinessLogic;


public class PersonalIdentificationCodeAttribute : ValidationAttribute
{
    private readonly string _birthDate;
    private readonly string _gender;

    public PersonalIdentificationCodeAttribute(string birthDate, string genderPyName)
    {
        _birthDate = birthDate;
        _gender = genderPyName;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var personalCode = value as string;
        if (string.IsNullOrEmpty(personalCode))
        {
            return new ValidationResult("Personal identification code is required.");
        }

        if (!Regex.IsMatch(personalCode, @"^\d{11}$"))
        {
            return new ValidationResult("Personal identification code must be exactly 11 digits.");
        }

        int centuryAndGender = int.Parse(personalCode.Substring(0, 1));
        if (centuryAndGender < 1 || centuryAndGender > 6)
        {
            return new ValidationResult("Invalid century and gender digit.");
        }

        string year = personalCode.Substring(1, 2);
        string month = personalCode.Substring(3, 2);
        string day = personalCode.Substring(5, 2);

        if (!int.TryParse(year, out int intYear) ||
            !int.TryParse(month, out int intMonth) ||
            !int.TryParse(day, out int intDay))
        {
            return new ValidationResult("Invalid date of birth part.");
        }

        int fullYear = GetFullYear(centuryAndGender, intYear);

        DateTime birthDateFromCode;
        try
        {
            birthDateFromCode = new DateTime(fullYear, intMonth, intDay);
        }
        catch
        {
            return new ValidationResult("Invalid date of birth.");
        }


        var birthDateProperty = validationContext.ObjectType.GetProperty(_birthDate);
        if (birthDateProperty == null)
        {
            throw new ArgumentException("Property with this name not found");
        }
        var birthDateValue = birthDateProperty.GetValue(validationContext.ObjectInstance, null);

        if (birthDateValue == null)
        {
            return new ValidationResult("Birth date is required.");
        }

        DateTime actualBirthDate = (DateTime)birthDateValue;

        if (birthDateFromCode != actualBirthDate)
        {
            return new ValidationResult("Birth date does not match the personal identification code.");
        }

        var genderProperty = validationContext.ObjectType.GetProperty(_gender);
        if (genderProperty == null)
        {
            throw new ArgumentException("Property with this name not found");
        }
        var genderValue = genderProperty.GetValue(validationContext.ObjectInstance, null);

        if (genderValue == null)
        {
            return new ValidationResult("Gender is required.");
        }

        string actualGender = genderValue.ToString().ToLower();
        if ((centuryAndGender % 2 == 1 && actualGender != "male") || (centuryAndGender % 2 == 0 && actualGender != "female"))
        {
            return new ValidationResult("Gender does not match the personal identification code.");
        }

        if (!ValidateCheckDigit(personalCode))
        {
            return new ValidationResult("Invalid personal identification code check digit.");
        }

        return ValidationResult.Success;


    }

    private int GetFullYear(int centuryAndGender, int year)
    {
        switch (centuryAndGender)
        {
            case 1:
            case 2:
                return 1800 + year;
            case 3:
            case 4:
                return 1900 + year;
            case 5:
            case 6:
                return 2000 + year;
            default:
                throw new ArgumentException("Invalid century and gender digit.");
        }
    }

    private bool ValidateCheckDigit(string personalCode)
    {
        int[] weights1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1 };
        int[] weights2 = { 3, 4, 5, 6, 7, 8, 9, 1, 2, 3 };

        int sum = CalculateSum(personalCode, weights1);
        int remainder = sum % 11;

        if (remainder != 10)
        {
            return remainder == int.Parse(personalCode[10].ToString());
        }

        sum = CalculateSum(personalCode, weights2);
        remainder = sum % 11;

        if (remainder != 10)
        {
            return remainder == int.Parse(personalCode[10].ToString());
        }

        return 0 == int.Parse(personalCode[10].ToString());
    }

    private int CalculateSum(string personalCode, int[] weights)
    {
        int sum = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            sum += int.Parse(personalCode[i].ToString()) * weights[i];
        }
        return sum;
    }
}
