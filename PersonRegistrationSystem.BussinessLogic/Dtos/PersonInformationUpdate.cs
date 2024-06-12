using System.ComponentModel.DataAnnotations;

namespace PersonRegistrationSystem.BussinessLogic;

public class PersonInformationUpdate
{
    [StringLength(50, MinimumLength = 2)]
    public required string Name { get; set; }

    [StringLength(50, MinimumLength = 2)]
    public required string LastName { get; set; }

    [RegularExpression("^(male|female)$", ErrorMessage = "Invalid gender specified.")]
    public string? Gender { get; set; }
    public DateTime Birthday { get; set; }
    public required string PersonalCode { get; set; }

    [StringLength(20, MinimumLength = 10)]
    public string? TelephoneNumber { get; set; }
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string? Email { get; set; }
    public PlaceOfResidenceDto? PlaceOfResidence { get; set; }
}
