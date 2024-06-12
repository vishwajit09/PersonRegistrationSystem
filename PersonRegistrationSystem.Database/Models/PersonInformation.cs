using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PersonRegistrationSystem.Database;

public class PersonInformation
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Gender { get; set; }
    public DateTime Birthday { get; set; }
    public string? PersonalCode { get; set; }
    public string? TelephoneNumber { get; set; }
    public string? Email { get; set; }
    public byte[]? ProfilePhoto { get; set; }
    public PlaceOfResidence? PlaceOfResidence { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public User User { get; set; }
}
