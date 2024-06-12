namespace PersonRegistrationSystem.BussinessLogic;

public interface IJwtService
{
  public string GetJwtToken(string userName, string userRole, string id);
}
