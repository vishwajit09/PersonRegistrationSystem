using System.ComponentModel.DataAnnotations.Schema;
namespace PersonRegistrationSystem.Database;

public class PlaceOfResidence
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? HouseNumber { get; set; }
    public string? ApartmentNumber { get; set; }
    [ForeignKey(nameof(PersonInformation))]
    public int PersonInformationId { get; set; }
    public PersonInformation? PersonInformation { get; set; } = null;
}
