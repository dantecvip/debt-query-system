using DebtQuerySystem.Infrastructure;
using DebtQuerySystem.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace DebtQuerySystem.Api.Configuration
{
    public static class MigrationConfiguration
    {
        public static async Task ApplyMigrations(this WebApplication app)
        {
            try
            {
                await app.Services.ApplyMigrationsAsync();
            }
            catch (Exception ex)
            {
                var httpClientFactory = app.Services.GetRequiredService<IHttpClientFactory>();
                var settings = app.Services.GetRequiredService<IOptions<AzureIntegrationSettings>>().Value;
                var env = app.Services.GetRequiredService<IWebHostEnvironment>();

                var payload = new
                {
                    service = "DebtQuerySystem.Api (Startup)",
                    environment = env.EnvironmentName,
                    destinationEmail = settings.ExceptionLogicAppsEmailDestination,
                    message = $"CRITICAL STARTUP ERROR: Falha ao aplicar as migrações do banco de dados. Detalhe: {ex.Message}",
                    stackTrace = env.IsDevelopment() ? ex.StackTrace : "StackTrace hidden in production.",
                    endpoint = "System Startup / ApplyMigrations",
                    timestamp = DateTime.UtcNow,
                    statusCode = 500
                };

                try
                {
                    var httpClient = httpClientFactory.CreateClient();
                    httpClient.Timeout = TimeSpan.FromSeconds(15);

                    JsonSerializerOptions jsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                    string jsonString = JsonSerializer.Serialize(payload, jsonOptions);

                    var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await httpClient.PostAsync(settings.ExceptionLogicAppsHttpTrigger, httpContent);

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorResponse = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"[Azure Logic Apps Error] Status: {response.StatusCode} - Resposta: {errorResponse}");
                    }
                }
                catch
                {
                    // Fail-safe caso a Azure também falhe
                }

                // Repropaga o erro para o container Docker cair com status de falha (Exit Code != 0)
                throw;
            }
        }
    }
}
