using Microsoft.Identity.Client;

namespace AP.MobileToolkit.AAD
{
    public interface IB2COptions
    {
        string Tenant { get; }
        string ClientId { get; }
        LogLevel? LogLevel { get; }
    }
}
