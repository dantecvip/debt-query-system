using DebtQuerySystem.Domain.Interfaces;
using DebtQuerySystem.Infrastructure.Database;
using DebtQuerySystem.Infrastructure.Database.Repository;
using DebtQuerySystem.Infrastructure.Services;
using DebtQuerySystem.Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.StackExchangeRedis;
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

        services.AddOptions<CacheSettings>().Bind(configuration.GetSection(CacheSettings.SectionName))
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

        services.AddOptions<RedisCacheOptions>()
            .Configure<IOptions<CacheSettings>>((options, cacheSettings) =>
            {
                options.Configuration = cacheSettings.Value.ConnectionString;
                options.InstanceName = cacheSettings.Value.InstanceName;
            });

        services.AddStackExchangeRedisCache(_ => { });

        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IDistributedCacheService, DistributedCacheService>();

        return services;
    }

    public static async Task ApplyMigrationsAsync(this IServiceProvider services)
    {
        Console.WriteLine("Aplicando migrações pendentes no banco de dados...");

        using var scope = services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<DebtQueryDbContext>();

        await db.Database.MigrateAsync();
    }
}
