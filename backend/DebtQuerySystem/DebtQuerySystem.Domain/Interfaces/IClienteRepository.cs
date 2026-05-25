using DebtQuerySystem.Domain.Entities;

namespace DebtQuerySystem.Domain.Interfaces;

public interface IClienteRepository
{
    Task<Cliente?> ObterPorCpfComDividasAsync(string cpf);
}