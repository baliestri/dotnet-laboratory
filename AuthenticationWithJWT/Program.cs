using AuthenticationWithJWT.EndPoints;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.MapWeatherForecastsEndPoints();

app.UseHttpsRedirection();

app.Run();
