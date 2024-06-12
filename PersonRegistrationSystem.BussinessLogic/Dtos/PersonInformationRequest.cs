namespace PersonRegistrationSystem.BussinessLogic;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
public class PersonInformationRequest
{
    [Required]
    [RegularExpression(@"^[a-zA-Z]{2,50}$", ErrorMessage = "Name must be between 2-50 characters and contain no special characters or numbers.")]
    public required string Name { get; set; }
    [Required]
    [RegularExpression(@"^[a-zA-Z]{2,50}$", ErrorMessage = "Last Name must be between 2-50 characters and contain no special characters or numbers.")]
    public required string LastName { get; set; }
    [Required]
    [RegularExpression("^(?i)(male|female)$", ErrorMessage = "Invalid gender specified.")]
    public string? Gender { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime Birthday { get; set; }

    [Required]
    [PersonalIdentificationCode("Birthday", "Gender")]
    public required string PersonalCode { get; set; }

    [StringLength(20, MinimumLength = 10)]

    [LithuaniaPhoneNumber]
    public string? TelephoneNumber { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [UniqueEmailAttribute]
    public string? Email { get; set; }

    [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = "Invalid file type. Only JPG and PNG files are allowed.")]
    public IFormFile? ProfilePhoto { get; set; }
    public PlaceOfResidenceDto? PlaceOfResidence { get; set; }



}

