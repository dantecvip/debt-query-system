namespace DebtQuerySystem.Domain.Entities;

public class Parcela
{
    public Guid Id { get; private set; }
    public Guid ProdutoId { get; private set; }
    public int ParcelaNumero { get; private set; }
    public decimal ValorOriginal { get; private set; }
    public DateOnly DataVencimento { get; private set; }
    public decimal TaxaAdministrativa { get; private set; }

    public int DiasAtraso => CalcularDiasAtraso();

    /// <summary>
    /// Incide 2% de multa FIXA sobre o valor original se houver atraso
    /// </summary>
    public decimal ValorMulta => DiasAtraso > 0 ? Math.Round(ValorOriginal * (2m / 100m), 2) : 0m;

    /// <summary>
    /// Incide 3% de juros AO DIA sobre o valor original baseado na quantidade de dias em atraso 
    /// </summary>
    public decimal ValorJuros => DiasAtraso > 0 ? Math.Round((ValorOriginal * (3m / 100m)) * DiasAtraso, 2) : 0m;

    public decimal ValorTotalAtualizado => ValorOriginal + ValorMulta + ValorJuros + TaxaAdministrativa;

    public int CalcularDiasAtraso(DateOnly? dataReferencia = null)
    {
        var hoje = dataReferencia ?? DateOnly.FromDateTime(DateTime.Today);
        return (hoje > DataVencimento) ? hoje.DayNumber - DataVencimento.DayNumber : 0;
    }

    protected Parcela() { }

    public Parcela(Guid produtoId, int parcelaNumero, decimal valorOriginal, DateOnly dataVencimento, decimal taxaAdministrativa)
    {
        if (valorOriginal <= 0)
            throw new ArgumentException("Valor original deve ser maior que zero.", nameof(valorOriginal));

        if (taxaAdministrativa <= 0)
            throw new ArgumentException("Taxa administrativa deve ser maior que zero.", nameof(taxaAdministrativa));

        if (parcelaNumero <= 0)
            throw new ArgumentException("Número da parcela deve ser maior que zero.", nameof(parcelaNumero));

        if (dataVencimento == default)
            throw new ArgumentException("Data de vencimento deve ser uma data válida.", nameof(dataVencimento));

        Id = Guid.NewGuid();
        ProdutoId = produtoId;
        ParcelaNumero = parcelaNumero;
        ValorOriginal = valorOriginal;
        DataVencimento = dataVencimento;
        TaxaAdministrativa = taxaAdministrativa;
    }
}
