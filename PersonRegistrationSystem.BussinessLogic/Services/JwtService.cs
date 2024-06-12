using System.Runtime.InteropServices;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;


using Microsoft.Extensions.Configuration;

using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;


namespace PersonRegistrationSystem.BussinessLogic;

public class JwtService : IJwtService
{
  private readonly IConfiguration _configuration;

  public JwtService(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public string GetJwtToken(string userName, string userRole, string id)
  {
    List<Claim> claims = new List<Claim>(){
        new Claim(ClaimTypes.Name,userName),
        new Claim(ClaimTypes.NameIdentifier,id),
        new Claim(ClaimTypes.Role,userRole)
    };

    var secretToken = _configuration.GetSection("Jwt:Key").Value;

#pragma warning disable CS8604 // Possible null reference argument.
    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretToken));
#pragma warning restore CS8604 // Possible null reference argument.

    var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

    var token = new JwtSecurityToken(
        issuer: _configuration.GetSection("Jwt:Issuer").Value,
        audience: _configuration.GetSection("Jwt:Audience").Value,
        claims: claims,
        expires: DateTime.Now.AddSeconds(30000),
        signingCredentials: cred
        );
    return new JwtSecurityTokenHandler().WriteToken(token);

  }
}
