namespace DebtQuerySystem.Api.Middlewares;

public static class ExceptionMiddlewareExtensions
{
    /// <summary>
    /// Adiciona o middleware global de tratamento de exceções integrado ao Azure Logic Apps no pipeline HTTP.
    /// </summary>
    public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}