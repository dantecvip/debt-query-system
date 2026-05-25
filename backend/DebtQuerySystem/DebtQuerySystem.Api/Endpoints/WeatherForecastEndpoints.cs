using DebtQuerySystem.Application.Queries.WeatherForecast;

namespace DebtQuerySystem.Api.Endpoints;

public static class WeatherForecastEndpoints
{
    internal static void MapWeatherForecastEndpoints(this WebApplication app)
    {
        app.MapGet("/", () => "API OK");
        app.MapGet("/api/v1/weatherforecast", GetWeatherForecastUseCase.Action).WithName(nameof(GetWeatherForecastUseCase));
    }
}
