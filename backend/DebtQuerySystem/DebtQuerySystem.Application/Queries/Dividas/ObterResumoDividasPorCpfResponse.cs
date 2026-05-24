namespace DebtQuerySystem.Application.Queries.Dividas;

public record ClienteResumoDebitosResult(string Nome, string Cpf, string Email, string Telefone, decimal ValorOriginalTotal, decimal ValorBaseAtualizadoTotal, decimal ValorTotalConsolidado, List<DividaResumoResult> Dividas);

public record DividaResumoResult(Guid Id, string Descricao, decimal ValorOriginalTotal, decimal ValorBaseAtualizadoTotal, decimal ValorTotalDividaAtualizada);