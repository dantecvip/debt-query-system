namespace DebtQuerySystem.Domain.Entities;

public class Produto
{
    public Guid Id { get; private set; }
    public Guid ClienteId { get; private set; }
    public string Descricao { get; private set; } = null!;
    private readonly List<Parcela> _parcelas = [];
    public IReadOnlyCollection<Parcela> Parcelas => _parcelas;

    public Parcela AdicionarParcela(
        int numero,
        decimal valorOriginal,
        DateOnly vencimento,
        decimal taxaAdministrativa)
    {
        var parcela = new Parcela(Id, numero, valorOriginal, vencimento, taxaAdministrativa);
        _parcelas.Add(parcela);
        return parcela;
    }

    protected Produto() { }

    public Produto(string descricao)
    {
        Id = Guid.NewGuid();
        Descricao = descricao;
    }
}
