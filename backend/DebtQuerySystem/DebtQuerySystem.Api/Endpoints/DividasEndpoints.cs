using DebtQuerySystem.Application.Queries.Dividas;

namespace DebtQuerySystem.Api.Endpoints
{
    public static class DividasEndpoints
    {
        internal static void MapDividasEndpoints(this WebApplication app)
        {
            app.MapGet("/api/v1/debitos/{cpf}", ObterDividasPorCpfUseCase.Action).WithName(nameof(ObterDividasPorCpfUseCase));
        }
    }
}
