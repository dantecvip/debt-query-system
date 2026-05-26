using DebtQuerySystem.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace DebtQuerySystem.Api.Middlewares;

public class ExceptionMiddleware(
    RequestDelegate next,
    IHttpClientFactory httpClientFactory,
    IWebHostEnvironment env,
    IOptions<AzureIntegrationSettings> options)
{
    private readonly RequestDelegate _next = next;
    private readonly IWebHostEnvironment _env = env;
    private readonly AzureIntegrationSettings _settings = options.Value;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = StatusCodes.Status500InternalServerError;

        var payload = new
        {
            service = "DebtQuerySystem.Api",
            environment = _env.EnvironmentName,
            destinationEmail = _settings.ExceptionLogicAppsEmailDestination ?? "john.doe@example.com",
            message = exception.Message,
            stackTrace = _env.IsDevelopment() ? exception.StackTrace : "StackTrace hidden in production.",
            endpoint = $"{context.Request.Method} {context.Request.Path}",
            timestamp = DateTime.UtcNow,
            statusCode
        };

        JsonSerializerOptions jsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        try
        {
            var httpClient = httpClientFactory.CreateClient();
            httpClient.Timeout = TimeSpan.FromSeconds(15);

            string jsonString = JsonSerializer.Serialize(payload, jsonOptions);

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            _ = await httpClient.PostAsync(_settings.ExceptionLogicAppsHttpTrigger, httpContent);
        }
        catch (Exception)
        {
            // Fail-safe: Garante que se o Logic App estiver fora do ar, 
            // a API não trave o fluxo de resposta do cliente
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new
        {
            status = statusCode,
            title = "An internal server error occurred",
            detail = _env.IsDevelopment() ? exception.Message : "Por favor, contate o suporte se o problema persistir.",
            instance = context.Request.Path.Value
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
    }
}