namespace DebtQuerySystem.Domain.Entities;

public class Cliente
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Cpf { get; private set; }
    public string Email { get; private set; }
    public string Telefone { get; private set; }
    public readonly List<Produto> _produtos = [];
    public IReadOnlyCollection<Produto> Produtos => _produtos;

    protected Cliente()
    {
        Nome = string.Empty;
        Cpf = string.Empty;
        Email = string.Empty;
        Telefone = string.Empty;
        _produtos = [];
    }

    public Cliente(string nome, string cpf, string email, string telephone)
    {
        if (cpf.Length < 11)
            throw new ArgumentException("CPF deve conter pelo menos 11 caracteres numéricos.", nameof(cpf));

        Id = Guid.NewGuid();
        Nome = nome;
        Cpf = new string([.. cpf.Where(char.IsDigit)]);
        Email = email;
        Telefone = telephone;
        _produtos = [];
    }
}
