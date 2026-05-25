namespace DebtQuerySystem.Infrastructure.Settings
{
    public class CorsSettings
    {
        public const string SectionName = "CorsSettings";

        public string[] Origins { get; set; } = [];
    }
}
