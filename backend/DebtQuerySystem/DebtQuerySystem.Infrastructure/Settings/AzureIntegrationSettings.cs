namespace DebtQuerySystem.Infrastructure.Settings;

public record AzureIntegrationSettings
{
    public const string SectionName = "AzureIntegrationSettings";

    public string ExceptionLogicAppsHttpTrigger { get; init; } = string.Empty;
    public string ExceptionLogicAppsEmailDestination { get; init; } = string.Empty;
}
