using DebtQuerySystem.Domain.Entities;
using DebtQuerySystem.DataSeeder.Reader;

namespace DebtQuerySystem.DataSeeder.Builder;

internal class ClienteDomainBuilder
{
    public static List<Cliente> Build(List<ClienteImportRow> rows)
    {
        var clientes = new List<Cliente>();

        var grupos = rows.GroupBy(x => x.Cpf);

        foreach (var grupoCliente in grupos)
        {
            var primeira = grupoCliente.First();

            var cliente = new Cliente(
                primeira.Nome,
                primeira.Cpf,
                primeira.Email,
                primeira.Telefone);

            Divida? dividaAtual = null;

            foreach (var item in grupoCliente)
            {
                var opcoesDescricaoFake = new[]
                {
                    "Empréstimo Pessoal",
                    "Financiamento de Veículo",
                    "Cartão de Crédito",
                    "Consórcio",
                    "Crédito Imobiliário"
                };

                if (item.Parcela == 1 || dividaAtual == null)
                {
                    dividaAtual = cliente.AdicionarDivida(opcoesDescricaoFake[
                        new Random().Next(opcoesDescricaoFake.Length)
                    ]);
                }

                dividaAtual.AdicionarParcela(
                    item.Parcela,
                    item.ValorOriginal,
                    item.DataVencimento,
                    item.TaxaAdministrativa);
            }

            clientes.Add(cliente);
        }

        return clientes;
    }
}
