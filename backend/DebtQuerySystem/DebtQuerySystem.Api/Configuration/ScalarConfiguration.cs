using Scalar.AspNetCore;

namespace DebtQuerySystem.Api.Configuration
{
    public static class ScalarConfiguration
    {
        public static WebApplication AddScalarConfiguration(this WebApplication app)
        {
            app.MapScalarApiReference(options =>
            {
                options.Title = "Debt Query System API";
                options.Theme = ScalarTheme.DeepSpace;

                options.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
            });

            return app;
        }
    }
}
