using AP.MobileToolkit.AAD;

namespace ToolkitDemo.Helpers
{
    public class AuthenticationOptions : IAuthenticationOptions
    {
        public string Tenant => Secrets.TenantName;

        public string ClientId => Secrets.ClientId;

        public string DefaultPolicy => Secrets.Policy;

        public string[] Scopes => new[] { Secrets.Scope };
    }
}
