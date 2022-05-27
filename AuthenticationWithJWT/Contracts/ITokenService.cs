using AuthenticationWithJWT.Models;

namespace AuthenticationWithJWT.Contracts;

public interface ITokenService {
  string GenerateToken(User user);
}
