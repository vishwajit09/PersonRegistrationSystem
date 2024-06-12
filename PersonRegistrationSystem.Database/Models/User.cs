
using System.ComponentModel.DataAnnotations.Schema;
namespace PersonRegistrationSystem.Database;


public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Username { get; set; }
    public byte[] Password { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string? Role { get; set; }
    public List<PersonInformation>? PersonInformation { get; set; } = new List<PersonInformation>();
}
