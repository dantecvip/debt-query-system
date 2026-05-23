using DebtQuerySystem.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using WatchDog;
using WatchDog.src.Enums;

namespace DebtQuerySystem.Api.Configuration;

public static class WatchDogConfiguration
{
    public static void AddWatchDog(WebApplicationBuilder builder)
    {
        var cfg = builder.Configuration.GetSection(DatabaseSettings.SectionName);

        builder.Services.AddOptions<DatabaseSettings>().Bind(builder.Configuration.GetSection(DatabaseSettings.SectionName))
            .Validate(s => !string.IsNullOrWhiteSpace(s.ConnectionString),
                "Connection string não configurada.")
            .ValidateOnStart();

        builder.Services.AddWatchDogServices(opt =>
        {
            using var serviceProvider = builder.Services.BuildServiceProvider();
            var dbSettings = serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;

            opt.IsAutoClear = true;
            opt.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Monthly;
            opt.SetExternalDbConnString = dbSettings.ConnectionString;
            opt.DbDriverOption = WatchDogDbDriverEnum.PostgreSql;
        });

        builder.Logging.AddWatchDogLogger();
    }

    public static void UseWatchDog(this WebApplication app)
    {
        app.UseWatchDogExceptionLogger();
        app.UseWatchDog(opt =>
        {
            opt.WatchPageUsername = "admin";
            opt.WatchPagePassword = "*Password123";
        });
    }
}
