using ClosedXML.Excel;
using System.Globalization;

namespace DebtQuerySystem.DataSeeder.Reader;

internal class ClienteExcelReader
{
    internal static List<ClienteImportRow> Read(string filePath)
    {
        var list = new List<ClienteImportRow>();

        string absolutePath = Path.IsPathRooted(filePath)
            ? filePath
            : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Seed", filePath);

        if (!File.Exists(absolutePath))
        {
            throw new FileNotFoundException($"[DataSeeder] Arquivo Excel não encontrado no caminho: {absolutePath}");
        }

        using var workbook = new XLWorkbook(absolutePath);
        var sheet = workbook.Worksheets.First();

        var rows = sheet.RangeUsed()?.RowsUsed().Skip(1);

        if (rows is null)
            return list;

        foreach (var row in rows)
        {
            list.Add(new ClienteImportRow
            {
                Nome = row.Cell(1).GetString(),
                Cpf = row.Cell(2).GetString(),
                Email = row.Cell(3).GetString(),
                Telefone = row.Cell(4).GetString(),
                Parcela = row.Cell(5).GetValue<int>(),
                ValorOriginal = decimal.Parse(
                    row.Cell(6).GetFormattedString(),
                    CultureInfo.InvariantCulture
                ),
                DataVencimento = DateOnly.FromDateTime(row.Cell(7).GetDateTime()),
                TaxaAdministrativa = decimal.Parse(
                    row.Cell(8).GetFormattedString(),
                    CultureInfo.InvariantCulture
                ),
            });
        }

        return list;
    }
}
