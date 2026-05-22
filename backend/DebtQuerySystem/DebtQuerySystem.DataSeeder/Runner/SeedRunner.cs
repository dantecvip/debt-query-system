using DebtQuerySystem.Infrastructure.Database;
using DebtQuerySystem.Infrastructure.Settings;
using DebtQuerySystem.DataSeeder.Builder;
using DebtQuerySystem.DataSeeder.Reader;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DebtQuerySystem.DataSeeder.Runner;

internal class SeedRunner(DebtQueryDbContext context, IOptions<DatabaseSettings> options)
{
    public async Task RunAsync()
    {
        Console.WriteLine("Applying migrations...");

        await context.Database.MigrateAsync();

        Console.WriteLine("Checking existing data...");

        if (await context.Clientes.AnyAsync())
        {
            Console.WriteLine("Data already seeded");
            return;
        }

        Console.WriteLine("Loading XLSX...");

        var rows = ClienteExcelReader.Read(options.Value.SeedPath);

        var clientes = ClienteDomainBuilder.Build(rows);

        await context.Clientes.AddRangeAsync(clientes);

        await context.SaveChangesAsync();

        Console.WriteLine("Seed completed");
    }
}
