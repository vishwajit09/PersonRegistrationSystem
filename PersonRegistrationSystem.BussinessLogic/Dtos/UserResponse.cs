namespace PersonRegistrationSystem.BussinessLogic;

public class UserResponse
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public List<PersonInformationdata> PersonInformationdata { get; set; }
}

public class PersonInformationdata()
{
    public required string Name { get; set; }
    public required string LastName { get; set; }

    public required string PersonId { get; set; }
}
