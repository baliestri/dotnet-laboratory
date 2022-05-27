using System.Text;

using AuthenticationWithJWT.Contracts;
using AuthenticationWithJWT.EndPoints;
using AuthenticationWithJWT.Repositories;
using AuthenticationWithJWT.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services
  .AddScoped<IUserRepository, UserRepository>()
  .AddScoped<ITokenService, TokenService>();

services
  .AddAuthorization()
  .AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  })
  .AddJwtBearer(x => {
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters {
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["tokenSecret"])),
      ValidateIssuer = false,
      ValidateAudience = false
    };
  });

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}

app
  .MapAuthEndPoints()
  .MapWeatherForecastsEndPoints();

app.UseHttpsRedirection();

app
  .UseAuthentication()
  .UseAuthorization();

app.Run();
