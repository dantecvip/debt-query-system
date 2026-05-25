using System;
using System.Collections.Generic;
using System.Text;

namespace DebtQuerySystem.Infrastructure.Settings
{
    public class KeycloakSettings
    {
        public const string SectionName = "KeycloakSettings";

        public string Authority { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}
