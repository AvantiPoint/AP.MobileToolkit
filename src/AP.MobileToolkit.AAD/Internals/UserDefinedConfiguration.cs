using Microsoft.Identity.Client;

namespace AP.MobileToolkit.AAD
{
    internal class UserDefinedConfiguration : IAuthConfiguration
    {
        private IAuthenticationOptions Options { get; }

        public UserDefinedConfiguration(IAuthenticationOptions options)
        {
            Options = options;
        }

        public string Authority => $"https://login.microsoftonline.com/tfp/{Options.Tenant.GetFullyQualifiedTenantName()}/{Policy}";
        public string Policy => Options.Policy;
        public string[] Scopes => Options.Scopes;
        public string ClientId => Options.ClientId;
        public string RedirectUri => $"msal{Options.ClientId}://auth";
        public LogLevel? LogLevel => Options.LogLevel;
        bool IAuthConfiguration.IsB2C => Options.IsB2C;
        public string TenantName => Options.Tenant.GetTenantName();
        public string FullyQualifiedTenantName => Options.Tenant.GetFullyQualifiedTenantName();
    }
}
