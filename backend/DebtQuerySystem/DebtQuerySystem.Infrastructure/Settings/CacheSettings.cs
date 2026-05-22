namespace DebtQuerySystem.Infrastructure.Settings;

public class CacheSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string InstanceName { get; set; } = string.Empty;
    public int DefaultExpirationMinutes { get; set; } = 20;
}
