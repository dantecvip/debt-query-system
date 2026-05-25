using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DebtQuerySystem.Application.Queries.WeatherForecast;

public static class GetWeatherForecastUseCase
{
    public static Ok<GetWeatherForecastResponse[]> Action()
    {
        var summaries = new[]
        {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy",
                "Hot", "Sweltering", "Scorching"
            };

        var forecast = Enumerable.Range(1, 5).Select(index =>
            new GetWeatherForecastResponse
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
            .ToArray();

        return TypedResults.Ok(forecast);
    }
}
