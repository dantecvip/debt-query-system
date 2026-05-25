namespace DebtQuerySystem.DataSeeder.Reader;

internal class ClienteImportRow
{
    public string Nome { get; set; } = "";
    public string Cpf { get; set; } = "";
    public string Email { get; set; } = "";
    public string Telefone { get; set; } = "";

    public int Parcela { get; set; }
    public decimal ValorOriginal { get; set; }
    public DateOnly DataVencimento { get; set; }
    public decimal TaxaAdministrativa { get; set; }
}
