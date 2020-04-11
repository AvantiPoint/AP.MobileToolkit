using Microsoft.Identity.Client;

namespace AP.MobileToolkit.AAD
{
    public interface IAuthenticationOptions
    {
        LogLevel? LogLevel { get; }
        string Tenant { get; }
        string Policy { get; }
        string[] Scopes { get; }
        string ClientId { get; }
        bool IsB2C { get; }
    }
}
