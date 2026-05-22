using DebtQuerySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DebtQuerySystem.Infrastructure.Database;

public class DebtQueryDbContext(DbContextOptions<DebtQueryDbContext> options) : DbContext(options)
{
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Divida> Dividas => Set<Divida>();
    public DbSet<Parcela> Parcelas => Set<Parcela>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DebtQueryDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
