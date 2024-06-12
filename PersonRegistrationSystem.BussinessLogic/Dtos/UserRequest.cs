using System.ComponentModel.DataAnnotations;

namespace PersonRegistrationSystem.BussinessLogic;

public class UserRequest
{
  [Required]
  [StringLength(20, MinimumLength = 8)]
  public required string Username { get; set; }

  [Required]
  [PasswordComplexity]
  public required string Password { get; set; }
}
