using DebtQuerySystem.Domain.Entities;
using DebtQuerySystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DebtQuerySystem.Infrastructure.Database.Repository;

public class ClienteRepository(DebtQueryDbContext context) : IClienteRepository
{
    public async Task<Cliente?> ObterPorCpfComDividasAsync(string cpf)
    {
        return await context.Clientes
            .AsNoTracking()
            .Include(c => c.Produtos)
            .ThenInclude(p => p.Parcelas)
            .FirstOrDefaultAsync(c => c.Cpf == cpf);
    }
}
