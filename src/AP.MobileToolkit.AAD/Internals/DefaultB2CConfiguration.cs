using Microsoft.Identity.Client;

namespace AP.MobileToolkit.AAD
{
    internal class DefaultB2CConfiguration : IAuthConfiguration
    {
        private IB2COptions Options { get; }

        public DefaultB2CConfiguration(IB2COptions options)
        {
            Options = options;
        }

        public string Authority => $"https://{Options.Tenant.GetTenantName()}.b2clogin.com/tfp/{Options.Tenant.GetFullyQualifiedTenantName()}/{Policy}";
        public string Policy => "B2C_1_SUSI";
        public string[] Scopes => new[] { $"https://{Options.Tenant.GetFullyQualifiedTenantName()}/mobile/read" };
        public string ClientId => Options.ClientId;
        public string RedirectUri => $"msal{Options.ClientId}://auth";
        public LogLevel? LogLevel => Options.LogLevel;
        bool IAuthConfiguration.IsB2C => true;
        public string TenantName => Options.Tenant.GetTenantName();
        public string FullyQualifiedTenantName => Options.Tenant.GetFullyQualifiedTenantName();
    }
}
