using AuthenticationWithJWT.Models;

namespace AuthenticationWithJWT.Contracts;

public interface IUserRepository {
  User? Get(string username, string password);
}
