using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using AuthenticationWithJWT.Contracts;
using AuthenticationWithJWT.Models;

using Microsoft.IdentityModel.Tokens;

namespace AuthenticationWithJWT.Services;

public class TokenService : ITokenService {
  private readonly IConfiguration _configuration;

  public TokenService(IConfiguration configuration)
    => _configuration = configuration;

  public string GenerateToken(User user) {
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.UTF8.GetBytes(_configuration["tokenSecret"]);
    var tokenDescriptor = new SecurityTokenDescriptor {
      Subject = new ClaimsIdentity(new Claim[] {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role)
      }),
      Expires = DateTime.UtcNow.AddMinutes(3),
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);

    return tokenHandler.WriteToken(token);
  }
}
