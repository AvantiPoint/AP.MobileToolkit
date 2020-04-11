namespace AP.MobileToolkit.AAD
{
    internal static class IAuthOptionsExtensions
    {
        internal static string RedirectUri(this IAuthenticationOptions options)
        {
            return $"msal{options.ClientId}://auth";
        }

        internal static string GetTenantName(this string tenant)
        {
            if (tenant.Split('.').Length > 1)
            {
                tenant = tenant.Split('.')[0];
            }

            return tenant.ToLower();
        }

        internal static string GetFullyQualifiedTenantName(this string tenant)
        {
            if (tenant.Split('.').Length == 1)
            {
                tenant = $"{tenant}.onmicrosoft.com";
            }

            return tenant.ToLower();
        }

        internal static string[] GetScopes(this IAuthenticationOptions options)
        {
            if (options.Scopes is null || options.Scopes.Length == 0)
            {
                return new[] { $"https://{options.Tenant.GetFullyQualifiedTenantName()}/mobile/read" };
            }

            return options.Scopes;
        }

        internal static string GetPolicy(this IAuthenticationOptions options)
        {
            return string.IsNullOrEmpty(options.Policy) ? "B2C_1_SUSI" : options.Policy;
        }

        internal static string GetB2CAuthority(this IAuthConfiguration config) =>
            config.GetB2CAuthority(config.Policy);

        internal static string GetB2CAuthority(this IAuthConfiguration config, string policy)
        {
            return $"https://{config.TenantName}.b2clogin.com/tfp/{config.FullyQualifiedTenantName}/{policy}";
        }
    }
}
