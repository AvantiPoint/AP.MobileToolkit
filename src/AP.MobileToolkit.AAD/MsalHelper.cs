using System;
using System.Diagnostics;
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

        public static IAuthenticationService CreateAAD<TOptions>(LogCallback logCallback)
            where TOptions : class, IAADOptions, new() =>
            Create(GetAADConfiguration<TOptions>(), logCallback);

        public static IAuthenticationService CreateB2C<TOptions>()
            where TOptions : class, IB2COptions, new() =>
            CreateB2C<TOptions>(NullLogCallback);

        public static IAuthenticationService CreateB2C<TOptions>(LogCallback logCallback)
            where TOptions : class, IB2COptions, new() =>
            Create(GetB2CConfiguration<TOptions>(), logCallback);

        public static IAuthenticationService Create<TOptions>()
            where TOptions : class, IAuthenticationOptions, new() =>
            Create<TOptions>(NullLogCallback);

        public static IAuthenticationService Create<TOptions>(LogCallback logCallback)
            where TOptions : class, IAuthenticationOptions, new() =>
            Create(GetConfiguration<TOptions>(), logCallback);

        public static IAuthenticationService Create(IB2COptions options, LogCallback logCallback)
        {
            var config = new DefaultB2CConfiguration(options);
            return Create(config, logCallback);
        }

        public static IAuthenticationService Create(IAADOptions options, LogCallback logCallback)
        {
            var config = new DefaultAADConfiguration(options);
            return Create(config, logCallback);
        }

        public static IAuthenticationService Create(IAuthenticationOptions options, LogCallback logCallback)
        {
            var config = new UserDefinedConfiguration(options);
            return Create(config, logCallback);
        }

        private static IAuthenticationService Create(IAuthConfiguration config, LogCallback logCallback) =>
            config.IsB2C ? CreateB2C(config, logCallback) : CreateAAD(config, logCallback);

        private static IAuthenticationService CreateAAD(IAuthConfiguration config, LogCallback logCallback)
        {
            var pca = CreateBaseBuilder(config, logCallback)
                        .Build();
            return new AuthenticationService(config, pca);
        }

        private static IAuthenticationService CreateB2C(IAuthConfiguration config, LogCallback logCallback)
        {
            var pca = CreateBaseBuilder(config, logCallback)
                .WithB2CAuthority(config.Authority)
                .Build();
            return new AuthenticationService(config, pca);
        }

        private static PublicClientApplicationBuilder CreateBaseBuilder(IAuthConfiguration configuration, LogCallback logCallback)
        {
            var redirectUri = string.IsNullOrEmpty(configuration.RedirectUri) ? $"msal{configuration.ClientId}://auth" : configuration.RedirectUri;
            var builder = PublicClientApplicationBuilder.Create(configuration.ClientId)
                                                 .WithRedirectUri(redirectUri);
#if __IOS__
            builder = builder.WithIosKeychainSecurityGroup(Xamarin.Essentials.AppInfo.PackageName);
#elif MONOANDROID
            builder = builder.WithParentActivityOrWindow(() => Xamarin.Essentials.Platform.CurrentActivity);
#endif
            return builder.WithLogging(logCallback ?? NullLogCallback, configuration.LogLevel);
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

        public static void TraceLogCallback(LogLevel logLevel, string message, bool containsPii)
        {
            if (!containsPii)
            {
                Trace.WriteLine($"{logLevel}: {message}");
            }
        }
    }
}
