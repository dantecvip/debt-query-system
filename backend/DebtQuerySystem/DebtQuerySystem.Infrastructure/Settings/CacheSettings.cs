namespace DebtQuerySystem.Infrastructure.Settings;

public class CacheSettings
{
    public string ExpirationSeconds { get; set; } = "1200"; // Valor padrão de 20 minutos (1200 segundos)
}
