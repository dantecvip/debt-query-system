namespace DebtQuerySystem.Domain.Entities;

public class Produto
{
    public Guid Id { get; private set; }
    public Guid ClienteId { get; private set; }
    public string Descricao { get; private set; }
    public readonly List<Parcela> _parcelas = [];
    public IReadOnlyCollection<Parcela> Parcelas => _parcelas;

    protected Produto()
    {
        Descricao = string.Empty;
        _parcelas = [];
    }

    public Produto(Guid clienteId, string descricao, List<Parcela> parcelas)
    {
        Id = Guid.NewGuid();
        ClienteId = clienteId;
        Descricao = descricao;
        _parcelas = parcelas;
    }
}
