using AuthenticationWithJWT.Contracts;
using AuthenticationWithJWT.Models;

namespace AuthenticationWithJWT.Repositories;

public class UserRepository : IUserRepository {
  private readonly List<User> _users;

  public UserRepository()
    => _users = new() {
      new User(1, "baliestri", "123456", "User"),
      new User(2, "brunnosalles", "123456", "Manager")
    };

  public User? Get(string username, string password)
    => _users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
}
