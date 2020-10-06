using System;
using Microsoft.Identity.Client;

namespace AP.MobileToolkit.AAD
{
    public static class MsalHelper
    {
        public static IAuthConfiguration GetConfiguration<TOptions>()
            where TOptions : class, IAuthenticationOptions, new()
        {
            return new UserDefinedConfiguration(Activator.CreateInstance<TOptions>());
        }

        public static IAuthConfiguration GetAADConfiguration<TOptions>()
            where TOptions : class, IAADOptions, new()
        {
            return new DefaultAADConfiguration(Activator.CreateInstance<TOptions>());
        }

        public static IAuthConfiguration GetB2CConfiguration<TOptions>()
            where TOptions : class, IB2COptions, new()
        {
            return new DefaultB2CConfiguration(Activator.CreateInstance<TOptions>());
        }

        public static IAuthenticationService CreateAAD<TOptions>()
            where TOptions : class, IAADOptions, new() =>
            CreateAAD<TOptions>(NullLogCallback);

        public static IAuthenticationService CreateAAD<TOptions>(LogCallback logCallback, Action<PublicClientApplicationBuilder> configureBuilder = null)
            where TOptions : class, IAADOptions, new() =>
            Create(GetAADConfiguration<TOptions>(), logCallback, configureBuilder);

        public static IAuthenticationService CreateB2C<TOptions>(Action<PublicClientApplicationBuilder> configureBuilder = null)
            where TOptions : class, IB2COptions, new() =>
            CreateB2C<TOptions>(default, configureBuilder);

        public static IAuthenticationService CreateB2C<TOptions>(LogCallback logCallback, Action<PublicClientApplicationBuilder> configureBuilder = null)
            where TOptions : class, IB2COptions, new() =>
            Create(GetB2CConfiguration<TOptions>(), logCallback, configureBuilder);

        public static IAuthenticationService Create<TOptions>(Action<PublicClientApplicationBuilder> configureBuilder = null)
            where TOptions : class, IAuthenticationOptions, new() =>
            Create<TOptions>(default, configureBuilder);

        public static IAuthenticationService Create<TOptions>(LogCallback logCallback, Action<PublicClientApplicationBuilder> configureBuilder = null)
            where TOptions : class, IAuthenticationOptions, new() =>
            Create(GetConfiguration<TOptions>(), logCallback, configureBuilder);

        public static IAuthenticationService Create(IB2COptions options, LogCallback logCallback, Action<PublicClientApplicationBuilder> configureBuilder = null)
        {
            var config = new DefaultB2CConfiguration(options);
            return Create(config, logCallback, configureBuilder);
        }

        public static IAuthenticationService Create(IAADOptions options, LogCallback logCallback, Action<PublicClientApplicationBuilder> configureBuilder = null)
        {
            var config = new DefaultAADConfiguration(options);
            return Create(config, logCallback, configureBuilder);
        }

        public static IAuthenticationService Create(IAuthenticationOptions options, LogCallback logCallback, Action<PublicClientApplicationBuilder> configureBuilder = null)
        {
            var config = new UserDefinedConfiguration(options);
            return Create(config, logCallback, configureBuilder);
        }

        private static IAuthenticationService Create(IAuthConfiguration config, LogCallback logCallback, Action<PublicClientApplicationBuilder> configurationCallback) =>
            config.IsB2C ? CreateB2C(config, logCallback, configurationCallback) : CreateAAD(config, logCallback, configurationCallback);

        private static IAuthenticationService CreateAAD(IAuthConfiguration config, LogCallback logCallback, Action<PublicClientApplicationBuilder> configurationCallback)
        {
            var pca = CreateBaseBuilder(config, logCallback, configurationCallback)
                        .Build();
            return new AuthenticationService(config, pca);
        }

        private static IAuthenticationService CreateB2C(IAuthConfiguration config, LogCallback logCallback, Action<PublicClientApplicationBuilder> configurationCallback)
        {
            var pca = CreateBaseBuilder(config, logCallback, configurationCallback)
                .WithB2CAuthority(config.Authority)
                .Build();
            return new AuthenticationService(config, pca);
        }

        private static PublicClientApplicationBuilder CreateBaseBuilder(IAuthConfiguration configuration, LogCallback logCallback, Action<PublicClientApplicationBuilder> configurationCallback)
        {
            var redirectUri = string.IsNullOrEmpty(configuration.RedirectUri) ? $"msal{configuration.ClientId}://auth" : configuration.RedirectUri;
            if (logCallback is null)
            {
                logCallback = NullLogCallback;

                if (configuration.LogLevel.HasValue)
                {
                    logCallback = ConsoleLogCallback;
                }
            }

            var builder = PublicClientApplicationBuilder.Create(configuration.ClientId)
                                                 .WithRedirectUri(redirectUri)
#if __IOS__
                                                 .WithIosKeychainSecurityGroup(Xamarin.Essentials.AppInfo.PackageName)
#elif MONOANDROID
                                                 .WithParentActivityOrWindow(() => Xamarin.Essentials.Platform.CurrentActivity)
#endif
                                                 .WithLogging(logCallback, configuration.LogLevel);
            configurationCallback?.Invoke(builder);
            return builder;
        }

        public static void NullLogCallback(LogLevel logLevel, string message, bool containsPii)
        {
        }

        public static void ConsoleLogCallback(LogLevel logLevel, string message, bool containsPii)
        {
            if (!containsPii)
            {
                Console.WriteLine($"{logLevel}: {message}");
            }
        }
    }
}
