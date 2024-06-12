using System.ComponentModel.DataAnnotations;

namespace PersonRegistrationSystem.BussinessLogic;

public class PlaceOfResidenceDto
{
    [Required]
    [CityAttribute]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City cannot contain numbers or special characters.")]
    public string? City { get; set; }

    [Required]
    [RegularExpression(@"^.+\s.+$", ErrorMessage = "Street must contain at least one space.")]
    public string? Street { get; set; }

    [Required]
    [RegularExpression(@"^[0-9]+[A-Za-z]?$", ErrorMessage = "Invalid house number format.")]
    public string? HouseNumber { get; set; }

    public string? ApartmentNumber { get; set; }

}
