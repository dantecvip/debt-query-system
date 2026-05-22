using DebtQuerySystem.Api.Configuration;
using DebtQuerySystem.Api.Endpoints;
using DebtQuerySystem.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);
LogConfiguration.AddSerilogConfiguration(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSerilogConfiguration();

app.UseHttpsRedirection();

app.MapWeatherForecastEndpoints();
app.MapDividasEndpoints();

await app.RunAsync();
