using Microsoft.Identity.Client;

namespace AP.MobileToolkit.AAD
{
    public interface IAADOptions
    {
        string Tenant { get; }
        string ClientId { get; }
        LogLevel? LogLevel { get; }
    }
}
