using DebtQuerySystem.Infrastructure;
using DebtQuerySystem.DataSeeder.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: true);
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddInfrastructure(context.Configuration);

        services.AddScoped<SeedRunner>();
    })
    .Build();

using var scope = host.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<SeedRunner>();

await seeder.RunAsync();
