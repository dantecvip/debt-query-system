namespace DebtQuerySystem.Application.Queries.Dividas;

public record ObterDividasPorCpfResponse(string NomeCliente, string CpfCliente, IList<DividaAgrupadaDto> Dividas)
{
    public decimal ValorTotalConsolidado => 
        Dividas.Sum(x => x.ValorTotalDividaAtualizada);
}

public record DividaAgrupadaDto(
    string DividaDescricao,
    List<ParcelaDetalheDto> Parcelas)
{
    public decimal ValorTotalDividaAtualizada =>
        Parcelas.Sum(x => x.ValorTotalAtualizado);
}

public class ParcelaDetalheDto
{
    public int ParcelaNumero { get; set; }
    public DateTime DataVencimento { get; set; }
    public decimal ValorOriginal { get; set; }
    public decimal ValorMulta { get; set; }
    public decimal ValorJuros { get; set; }
    public decimal TaxaAdministrativa { get; set; }
    public int DiasAtraso { get; set; }
    public decimal ValorTotalAtualizado { get; set; }
}