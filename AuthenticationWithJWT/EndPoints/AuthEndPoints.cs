using AuthenticationWithJWT.Contracts;
using AuthenticationWithJWT.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationWithJWT.EndPoints;

public static class AuthEndPoints {
  public static WebApplication MapAuthEndPoints(this WebApplication app) {
    app.MapPost("/login", Authenticate);
    app.MapGet("/authenticated", Authenticated);

    return app;
  }

  [AllowAnonymous]
  private static dynamic Authenticate(
    [FromServices] IUserRepository userRepository,
    [FromServices] ITokenService tokenService,
    [FromBody] User model
  ) {
    var user = userRepository.Get(model.Username, model.Password);

    if (user is null)
      return Results.NotFound(new { message = "Invalid information" });

    var token = tokenService.GenerateToken(user);

    return new {
      UserInfo = new {
        Id = user.Id,
        Username = user.Username,
        Role = user.Role
      },
      Token = token
    };
  }

  [Authorize]
  public static object Authenticated()
    => new {
      message = $"Autenticado"
    };
}
