using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


namespace PersonRegistrationSystem;

public class JwtServices
{
  private readonly IConfiguration _configuration;

  public JwtServices(IConfiguration configuration){
    _configuration = configuration;
  }

  public string GetJwtToken(string userName , String userRole)
  {
    List<Claim> claims=  new List<Claim>()
    {
        new Claim(ClaimTypes.Name, userName),
        new Claim(ClaimTypes.Role, userRole)
    };
    var secretToken =_configuration.GetSection("Jwt:key").Value;
    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretToken));

    var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("Jwt:Issuer").Value,
                audience: _configuration.GetSection("Jwt:Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddSeconds(300),                
                signingCredentials: cred
                
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
  }
}
