namespace DebtQuerySystem.Application.Queries.Dividas;

public record ClienteDebitosResult(string Nome, string Cpf, List<DividaResult> Dividas)
{
    public decimal ValorTotalConsolidado => 
        Dividas.Sum(x => x.ValorTotalDividaAtualizada);
}

public record DividaResult(string Descricao, List<ParcelaResult> Parcelas)
{
    public decimal ValorTotalDividaAtualizada =>
        Parcelas.Sum(x => x.ValorTotalAtualizado);
}

public record ParcelaResult(
    int NumeroParcela,
    decimal ValorOriginal,
    DateOnly DataVencimento,
    int DiasAtraso,
    decimal ValorMulta,
    decimal ValorJuros,
    decimal ValorTotalAtualizado);