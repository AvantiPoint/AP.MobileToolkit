namespace AP.MobileToolkit.AAD
{
    internal static class IAuthenticationOptionsExtensions
    {
        public static string GetTenant(this IAuthenticationOptions options)
        {
            var domainParts = options.Tenant.Split('.');
            if (domainParts.Length == 1)
                return $"{options.Tenant.ToLower()}.onmicrosoft.com";

            return options.Tenant.ToLower();
        }

        public static string GetB2CAuthority(this IAuthenticationOptions options, string policy)
        {
            return $"https://login.microsoftonline.com/tfp/{options.GetTenant()}/{policy}";
        }
    }
}
