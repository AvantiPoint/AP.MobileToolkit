using Microsoft.Identity.Client;

namespace AP.MobileToolkit.AAD
{
    internal class DefaultAADConfiguration : IAuthConfiguration
    {
        private IAADOptions Options { get; }

        public DefaultAADConfiguration(IAADOptions options)
        {
            Options = options;
        }

        public string Authority => $"https://login.microsoftonline.com/tfp/{Options.Tenant.GetFullyQualifiedTenantName()}/{Policy}";
        public string Policy => string.Empty;
        public string[] Scopes => new[] { $"User.Read" };
        public string ClientId => Options.ClientId;
        public string RedirectUri => $"msal{Options.ClientId}://auth";
        public LogLevel? LogLevel => Options.LogLevel;
        bool IAuthConfiguration.IsB2C => false;
        public string TenantName => Options.Tenant.GetTenantName();
        public string FullyQualifiedTenantName => Options.Tenant.GetFullyQualifiedTenantName();
    }
}
