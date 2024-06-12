namespace PersonRegistrationSystem.BussinessLogic;

public class PersonInformationResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public string? Gender { get; set; }
    public DateTime Birthday { get; set; }
    public required string PersonalCode { get; set; }
    public string? TelephoneNumber { get; set; }
    public string? Email { get; set; }
    public PlaceOfResidenceDto? PlaceOfResidence { get; set; }

}
