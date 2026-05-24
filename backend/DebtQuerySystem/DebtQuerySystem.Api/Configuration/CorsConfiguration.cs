using DebtQuerySystem.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace DebtQuerySystem.Api.Configuration;

public static class CorsConfiguration
{
    public static void AddCorsConfiguration(WebApplicationBuilder builder)
    {
        var cfg = builder.Configuration.GetSection(CorsSettings.SectionName);

        builder.Services.AddOptions<CorsSettings>().Bind(builder.Configuration.GetSection(CorsSettings.SectionName))
            .Validate(s => s.Origins.Length > 0,
                "Origins não configuradas")
            .ValidateOnStart();

        builder.Services.AddCors(options =>
        {
            using var serviceProvider = builder.Services.BuildServiceProvider();
            var corsSettings = serviceProvider.GetRequiredService<IOptions<CorsSettings>>().Value;

            options.AddPolicy("frontend", policy =>
            {
                policy.WithOrigins(corsSettings.Origins)
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });
    }

    public static WebApplication UseCorsConfiguration(this WebApplication app)
    {
        app.UseCors("frontend");
        return app;
    }
}