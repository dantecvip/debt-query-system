using DebtQuerySystem.Domain.Entities;

namespace DebtQuerySystem.Tests.Domain;

public class ParcelaTests
{
    [Theory(DisplayName = "Deve calcular corretamente os dias de atraso com base na data de referência")]
    [InlineData("2026-05-10", "2026-05-20", 10)]
    [InlineData("2026-05-20", "2026-05-20", 0)]
    [InlineData("2026-06-25", "2026-05-20", 0)]
    public void DiasAtraso_QuandoComparadoComDataReferencia_DeveCalcularDiferencaExata(
    string dataVencimentoStr, string dataReferenciaStr, int diasEsperados)
    {
        // Arrange
        var dataVencimento = DateOnly.Parse(dataVencimentoStr);
        var dataReferencia = DateOnly.Parse(dataReferenciaStr);
        var produtoId = Guid.NewGuid();

        var divida = new Parcela(produtoId, 1, 100.00m, dataVencimento, 1m);

        // Act
        int diasObtidos = divida.CalcularDiasAtraso(dataReferencia);

        // Assert
        Assert.Equal(diasEsperados, diasObtidos);
    }

    [Theory(DisplayName = "Deve calcular corretamente a multa quando houver atraso")]
    [InlineData(200, 0, 0)]
    [InlineData(200, 2, 4)]
    [InlineData(100, 3, 2)]
    [InlineData(100, 30, 2)]
    public void ValorMulta_QuandoHaAtraso_DeveCalcularMultaCorretamente(decimal valorOriginal, 
        int diasAtraso, decimal valorMultaEsperado)
    {
        // Arrange
        var dataVencimento = DateOnly.FromDateTime(DateTime.Today.AddDays(diasAtraso > 0 ? diasAtraso * -1 : 0));
        var produtoId = Guid.NewGuid();
        var parcela = new Parcela(produtoId, 1, valorOriginal, dataVencimento, 1m);

        // Act
        decimal valorMultaObtido = parcela.ValorMulta;

        // Assert
        Assert.Equal(valorMultaEsperado, valorMultaObtido);
    }

    [Theory(DisplayName = "Deve calcular corretamente os juros quando houver atraso")]
    [InlineData(100, 0, 0)]
    [InlineData(100, 1, 3)]
    [InlineData(100, 3, 9)]
    [InlineData(150, 2, 9)]
    [InlineData(216, 10, 64.8)]
    public void ValorJuros_QuandoHaAtraso_DeveCalcularJurosCorretamente(decimal valorOriginal,
        int diasAtraso, decimal valorJurosEsperado)
    {
        // Arrange
        var dataVencimento = DateOnly.FromDateTime(DateTime.Today.AddDays(diasAtraso > 0 ? diasAtraso * -1 : 0));
        var produtoId = Guid.NewGuid();
        var parcela = new Parcela(produtoId, 1, valorOriginal, dataVencimento, 1m);

        // Act
        decimal valorJurosObtido = parcela.ValorJuros;

        // Assert
        Assert.Equal(valorJurosEsperado, valorJurosObtido);
    }

    [Theory(DisplayName = "Deve calcular o valor total atualizado quando houver atraso")]
    [InlineData(100, 0, 47.4, 147.4)]
    [InlineData(100, 1, 50.5, 155.5)]
    [InlineData(100, 2, 50.5, 158.5)]
    [InlineData(200, 7, 68.8, 314.8)]
    [InlineData(230, 30, 32.3, 473.9)]
    public void ValorTotalAtualizado_QuandoHaAtraso_DeveCalcularValorTotalCorretamente(decimal valorOriginal,
        int diasAtraso, decimal taxaAdministrativa, decimal valorTotalEsperado)
    {
        // Arrange
        var dataVencimento = DateOnly.FromDateTime(DateTime.Today.AddDays(diasAtraso > 0 ? diasAtraso * -1 : 0));
        var produtoId = Guid.NewGuid();
        var parcela = new Parcela(produtoId, 1, valorOriginal, dataVencimento, taxaAdministrativa);

        // Act
        decimal valorTotalObtido = parcela.ValorTotalAtualizado;

        // Assert
        Assert.Equal(valorTotalEsperado, valorTotalObtido);
    }
}
