using DebtQuerySystem.Application.Queries.Dividas;
using DebtQuerySystem.Domain.Entities;

namespace DebtQuerySystem.Application.Transformers
{
    public static class DividaTransformer
    {
        public static ClienteDebitosResult ToClienteDebitosResult(Cliente cliente)
        {
            var resultado = new ClienteDebitosResult
                (
                    cliente.Nome,
                    cliente.Cpf,
                    cliente.Email,
                    cliente.Telefone,
                    cliente.Dividas.Select(d => new DividaResult
                    (
                        d.Id,
                        d.Descricao,
                        d.Parcelas.Select(p => new ParcelaResult
                        (
                            p.Id,
                            p.ParcelaNumero,
                            p.ValorOriginal,
                            p.DataVencimento,
                            p.DiasAtraso,
                            p.ValorMulta,
                            p.ValorJuros,
                            p.TaxaAdministrativa,
                            p.ValorTotalAtualizado
                        )).ToList()
                    )).ToList()
                );

            return resultado;
        }

        public static ClienteResumoDebitosResult ToClienteResumoDebitosResult(ClienteDebitosResult clienteDebitosResult)
        {
            var resultado = new ClienteResumoDebitosResult
                (
                    clienteDebitosResult.Nome,
                    clienteDebitosResult.Cpf,
                    clienteDebitosResult.Email,
                    clienteDebitosResult.Telefone,
                    clienteDebitosResult.Dividas.Sum(d => d.Parcelas.Sum(p => p.ValorOriginal)),
                    clienteDebitosResult.Dividas.Sum(d => d.Parcelas.Sum(p => p.ValorOriginal + p.TaxaAdministrativa)),
                    clienteDebitosResult.Dividas.Sum(d => d.Parcelas.Sum(p => p.ValorTotalAtualizado)),
                    clienteDebitosResult.Dividas.Select(d => new DividaResumoResult
                    (
                        d.Id,
                        d.Descricao,
                        d.Parcelas.Sum(p => p.ValorOriginal),
                        d.Parcelas.Sum(p => p.ValorOriginal + p.TaxaAdministrativa),
                        d.Parcelas.Sum(p => p.ValorTotalAtualizado)
                    )).ToList()
                );

            return resultado;
        }
    }
}
