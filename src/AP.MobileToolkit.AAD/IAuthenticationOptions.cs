namespace AP.MobileToolkit.AAD
{
    public interface IAuthenticationOptions
    {
        string Tenant { get; }

        string ClientId { get; }

        string DefaultPolicy { get; }

        string[] Scopes { get; }
    }
}
