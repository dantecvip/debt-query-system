namespace DebtQuerySystem.Application.Queries.Dividas;

public record ObterDividasPorCpfResponse(string NomeCliente, string CpfCliente, IList<ProdutoAgrupadoDto> Produtos)
{
    public decimal ValorTotalConsolidado => 
        Produtos.Sum(x => x.ValorTotalProdutoAtualizado);
}

public record ProdutoAgrupadoDto(
    string ProdutoNome,
    List<ParcelaDetalheDto> Parcelas)
{
    public decimal ValorTotalProdutoAtualizado =>
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