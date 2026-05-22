using DebtQuerySystem.Domain.Interfaces;
using DebtQuerySystem.Infrastructure.Database;
using DebtQuerySystem.Infrastructure.Database.Repository;
using DebtQuerySystem.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DebtQuerySystem.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var cfg = configuration.GetSection(DatabaseSettings.SectionName);

        services.AddOptions<DatabaseSettings>().Bind(configuration.GetSection(DatabaseSettings.SectionName))
            .Validate(s => !string.IsNullOrWhiteSpace(s.ConnectionString),
                "Connection string não configurada.")
            .ValidateOnStart();

        services.AddDbContext<DebtQueryDbContext>((provider, options) =>
        {
            var databaseSettings = provider
                .GetRequiredService<IOptions<DatabaseSettings>>()
                .Value;

            options.UseNpgsql(databaseSettings.ConnectionString);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        services.AddScoped<IClienteRepository, ClienteRepository>();

        return services;
    }
}
