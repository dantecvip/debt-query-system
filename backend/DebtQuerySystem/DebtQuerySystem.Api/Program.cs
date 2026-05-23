using DebtQuerySystem.Api.Configuration;
using DebtQuerySystem.Api.Endpoints;
using DebtQuerySystem.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);
SerilogConfiguration.AddSerilogConfiguration(builder);
WatchDogConfiguration.AddWatchDog(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.AddScalarConfiguration();
}

app.UseSerilogConfiguration();
app.UseWatchDog();

app.UseHttpsRedirection();

app.MapWeatherForecastEndpoints();
app.MapDividasEndpoints();

await app.RunAsync();
