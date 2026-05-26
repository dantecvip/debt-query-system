using DebtQuerySystem.Api.Configuration;
using DebtQuerySystem.Api.Endpoints;
using DebtQuerySystem.Api.Middlewares;
using DebtQuerySystem.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddHttpClient();
builder.Services.AddInfrastructure(builder.Configuration);
CorsConfiguration.AddCorsConfiguration(builder);
SerilogConfiguration.AddSerilogConfiguration(builder);
AuthConfiguration.AddAuthConfiguration(builder);

var app = builder.Build();

app.UseCustomExceptionMiddleware();

await app.ApplyMigrations();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.AddScalarConfiguration();
}

app.UseCorsConfiguration();
app.UseSerilogConfiguration();
app.UseAuthConfiguration();

app.MapWeatherForecastEndpoints();
app.MapDividasEndpoints();

await app.RunAsync();
