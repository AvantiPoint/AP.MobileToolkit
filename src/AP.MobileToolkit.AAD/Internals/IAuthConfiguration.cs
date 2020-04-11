using Microsoft.Identity.Client;

namespace AP.MobileToolkit.AAD
{
    public interface IAuthConfiguration
    {
        string TenantName { get; }
        string FullyQualifiedTenantName { get; }
        string Authority { get; }
        string Policy { get; }
        string[] Scopes { get; }
        string ClientId { get; }
        string RedirectUri { get; }
        LogLevel? LogLevel { get; }
        bool IsB2C { get; }
    }
}
