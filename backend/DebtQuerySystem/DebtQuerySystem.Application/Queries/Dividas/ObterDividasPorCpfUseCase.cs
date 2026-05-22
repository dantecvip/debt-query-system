using DebtQuerySystem.Domain.Interfaces;
using DebtQuerySystem.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace DebtQuerySystem.Application.Queries.Dividas
{
    public class ObterDividasPorCpfUseCase
    {
        public async static Task<Results<Ok<ClienteDebitosResult>, NotFound>> Action(string cpf,
            IClienteRepository clienteRepository,
            IDistributedCacheService cacheService,
            ILogger<ObterDividasPorCpfUseCase> logger)
        {
            var cpfLimpo = new string(cpf.Where(char.IsDigit).ToArray());
            var cacheKey = $"cliente:{cpfLimpo}";

            logger.LogInformation("Iniciando busca de débitos para o CPF: {Cpf}", cpfLimpo);

            var clienteCacheado = await cacheService.GetAsync(cacheKey);
            if (!string.IsNullOrEmpty(clienteCacheado))
            {
                logger.LogInformation("Cache encontrado para o CPF: {Cpf}. Retornando dados do Redis.", cpfLimpo);

                return TypedResults.Ok(JsonSerializer.Deserialize<ClienteDebitosResult>(clienteCacheado));
            }

            logger.LogWarning("Cache não encontrado para o CPF: {Cpf}. Buscando dados no PostgreSQL.", cpfLimpo);

            var clienteDebitosDb =
                await clienteRepository.ObterPorCpfComDividasAsync(cpf);

            if (clienteDebitosDb == null)
            {
                logger.LogWarning("Cliente com o CPF: {Cpf} não foi encontrado no banco de dados.", cpfLimpo);

                return TypedResults.NotFound();
            }

            var resultado = new ClienteDebitosResult
                (
                    clienteDebitosDb.Nome,
                    clienteDebitosDb.Cpf,
                    clienteDebitosDb.Dividas.Select(d => new DividaResult
                    (
                        d.Descricao,
                        d.Parcelas.Select(p => new ParcelaResult
                        (
                            p.ParcelaNumero,
                            p.ValorOriginal,
                            p.DataVencimento,
                            p.DiasAtraso,
                            p.ValorMulta,
                            p.ValorJuros,
                            p.ValorTotalAtualizado
                        )).ToList()
                    )).ToList()
                );

            try
            {
                var jsonParaCache = JsonSerializer.Serialize(resultado);
                await cacheService.SetAsync(cacheKey, jsonParaCache);
                logger.LogInformation("Dados do CPF: {Cpf} calculados e persistidos no Redis com sucesso.", cpfLimpo);
            }
            catch (Exception ex)
            {
                // Se o Redis falhar por algum motivo, o sistema NÃO deve quebrar (Fail-Safe)
                // Apenas logamos como erro e o fluxo segue entregando os dados do banco
                logger.LogError(ex, "Falha ao tentar salvar os dados do CPF: {Cpf} no Redis.", cpfLimpo);
            }

            return TypedResults.Ok(resultado);
        }
    }
}
