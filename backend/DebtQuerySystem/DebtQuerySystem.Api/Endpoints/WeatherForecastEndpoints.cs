using DebtQuerySystem.Application.WeatherForecast;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DebtQuerySystem.Api.Endpoints
{
    public static class WeatherForecastEndpoints
    {
        internal static void MapWeatherForecastEndpoints(this WebApplication app)
        {
            app.MapGet("/weatherforecast", GetWeatherForecastUseCase.Action).WithName(nameof(GetWeatherForecastUseCase));
        }
    }
}
