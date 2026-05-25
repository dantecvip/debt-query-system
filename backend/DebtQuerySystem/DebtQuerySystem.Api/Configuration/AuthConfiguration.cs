using DebtQuerySystem.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DebtQuerySystem.Api.Configuration
{
    public static class AuthConfiguration
    {
        public static void AddAuthConfiguration(WebApplicationBuilder builder)
        {
            var cfg = builder.Configuration.GetSection(CorsSettings.SectionName);

            builder.Services.AddOptions<KeycloakSettings>().Bind(builder.Configuration.GetSection(KeycloakSettings.SectionName))
                .Validate(s => !string.IsNullOrEmpty(s.Authority) && !string.IsNullOrEmpty(s.Audience), 
                "Keycloak não configurado")
                .ValidateOnStart();

            builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                using var serviceProvider = builder.Services.BuildServiceProvider();
                var keycloakSettings = serviceProvider.GetRequiredService<IOptions<KeycloakSettings>>().Value;

                Console.WriteLine(keycloakSettings.Authority);

                options.Authority = keycloakSettings.Authority;
                options.Audience = keycloakSettings.Audience;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false // Evita rejeição por incompatibilidade de nomes (localhost vs keycloak)
                };
            });
            builder.Services.AddAuthorization();
        }

        public static void UseAuthConfiguration(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
