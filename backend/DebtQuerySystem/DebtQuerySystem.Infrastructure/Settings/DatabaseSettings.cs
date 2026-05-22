namespace DebtQuerySystem.Infrastructure.Settings;

public class DatabaseSettings
{
    public const string SectionName = "DatabaseSettings";

    public string ConnectionString { get; set; } = string.Empty;
    public string SeedPath { get; set; } = string.Empty;
}
