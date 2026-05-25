namespace DebtQuerySystem.Application.Queries.Dividas;

public record ClienteDebitosResult(string Nome, string Cpf, string Email, string Telefone, List<DividaResult> Dividas)
{
    public decimal ValorOriginalTotal =>
        Dividas.Sum(x => x.ValorOriginalTotal);

    public decimal ValorBaseAtualizadoTotal =>
        Dividas.Sum(x => x.ValorBaseAtualizadoTotal);

    public decimal ValorTotalConsolidado => 
        Dividas.Sum(x => x.ValorTotalDividaAtualizada);
}

public record DividaResult(Guid Id, string Descricao, List<ParcelaResult> Parcelas)
{
    public decimal ValorOriginalTotal => Parcelas != null && Parcelas.Count > 0 ?
        Parcelas.Sum(x => x.ValorOriginal) : 0;

    public decimal ValorBaseAtualizadoTotal => Parcelas != null && Parcelas.Count > 0 ?
        Parcelas.Sum(x => x.ValorOriginal + x.TaxaAdministrativa) : 0;

    public decimal ValorTotalDividaAtualizada => Parcelas != null && Parcelas.Count > 0 ?
        Parcelas.Sum(x => x.ValorTotalAtualizado) : 0;
}

public record ParcelaResult(
    Guid Id,
    int ParcelaNumero,
    decimal ValorOriginal,
    DateOnly DataVencimento,
    int DiasAtraso,
    decimal ValorMulta,
    decimal ValorJuros,
    decimal TaxaAdministrativa,
    decimal ValorTotalAtualizado);