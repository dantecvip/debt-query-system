using DebtQuerySystem.Api.Configuration;
using DebtQuerySystem.Api.Endpoints;
using DebtQuerySystem.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);
CorsConfiguration.AddCorsConfiguration(builder);
SerilogConfiguration.AddSerilogConfiguration(builder);
WatchDogConfiguration.AddWatchDogConfiguration(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.AddScalarConfiguration();
}

app.UseHttpsRedirection();

app.UseCorsConfiguration();
app.UseSerilogConfiguration();
app.UseWatchDogConfiguration();

app.MapWeatherForecastEndpoints();
app.MapDividasEndpoints();

await app.RunAsync();
