using AuthenticationWithJWT.Models;

using Microsoft.AspNetCore.Authorization;

namespace AuthenticationWithJWT.EndPoints;

public static class WeatherForecastsEndPoints {
  private static string[] Summaries = new[] {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
  };

  public static WebApplication MapWeatherForecastsEndPoints(this WebApplication app) {
    app.MapGet("/weatherforecast", GetWeatherForecast).WithName(nameof(GetWeatherForecast));

    return app;
  }

  [Authorize(Roles = "Manager")]
  private static WeatherForecast[] GetWeatherForecast() {
    var forecast = Enumerable.Range(1, 5).Select(index =>
      new WeatherForecast
      (
          DateTime.Now.AddDays(index),
          Random.Shared.Next(-20, 55),
          Summaries[Random.Shared.Next(Summaries.Length)]
      ))
      .ToArray();

    return forecast;
  }
}
