namespace DebtQuerySystem.Domain.Entities;

public class Cliente
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = null!;
    public string Cpf { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Telefone { get; private set; } = null!;
    private readonly List<Produto> _produtos = [];
    public IReadOnlyCollection<Produto> Produtos => _produtos;

    public Produto AdicionarProduto(string descricao)
    {
        var produto = new Produto(descricao);
        _produtos.Add(produto);
        return produto;
    }

    protected Cliente() { }

    public Cliente(string nome, string cpf, string email, string telefone)
    {
        cpf = ApenasNumeros(cpf);

        if (!ValidarCpf(cpf))
            throw new ArgumentException("CPF deve conter 11 digitos.", nameof(cpf));

        Id = Guid.NewGuid();
        Nome = nome;
        Cpf = cpf;
        Email = email.Trim().ToLowerInvariant();
        Telefone = telefone.Trim();
    }

    #region Private Methods

    private static bool ValidarCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11)
            return false;

        // elimina CPFs com todos dígitos iguais
        if (cpf.Distinct().Count() == 1)
            return false;

        var numbers = cpf.Select(c => int.Parse(c.ToString())).ToArray();

        // primeiro dígito verificador
        int sum1 = 0;
        for (int i = 0; i < 9; i++)
            sum1 += numbers[i] * (10 - i);

        int digit1 = (sum1 * 10) % 11;
        if (digit1 == 10) digit1 = 0;

        if (numbers[9] != digit1)
            return false;

        // segundo dígito verificador
        int sum2 = 0;
        for (int i = 0; i < 10; i++)
            sum2 += numbers[i] * (11 - i);

        int digit2 = (sum2 * 10) % 11;
        if (digit2 == 10) digit2 = 0;

        if (numbers[10] != digit2)
            return false;

        return true;
    }

    private static string ApenasNumeros(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        return new string(input.Where(char.IsDigit).ToArray());
    }

    #endregion
}
