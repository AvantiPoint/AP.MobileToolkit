using Microsoft.Identity.Client;
using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#if __ANDROID__
using Plugin.CurrentActivity;
#endif

namespace AP.MobileToolkit.AAD
{
    public sealed class AuthenticationService : IAuthenticationService
    {
        private IPublicClientApplication AuthClient { get; }

        private IAuthenticationOptions Options { get; }

        private ILogger Logger { get; }

#if __ANDROID__
        private ICurrentActivity CurrentActivity { get; }

        public AuthenticationService(IAuthenticationOptions authenticationOptions, ILogger logger, IPublicClientApplication publicClientApplication)
        {
            Options = authenticationOptions;
            Logger = logger;
            AuthClient = publicClientApplication;
        }
#else
        public AuthenticationService(IAuthenticationOptions authenticationOptions, ILogger logger, IPublicClientApplication publicClientApplication)
        {
            Options = authenticationOptions;
            Logger = logger;
            AuthClient = publicClientApplication;
        }
#endif

        public Task<IEnumerable<IAccount>> GetAccountsAsync() => AuthClient.GetAccountsAsync();

        public Task<AuthenticationResult> LoginAsync() => LoginAsync(Options.DefaultPolicy);

        public async Task<AuthenticationResult> LoginAsync(string policy)
        {
            AuthenticationResult result;
            try
            {
                result = await SilentLoginAsync(policy);

                if (result != null)
                    return result;

                result = await AuthClient.AcquireTokenInteractive(Options.Scopes)
                                         .WithB2CAuthority(Options.GetB2CAuthority(policy))
                                         .WithUseEmbeddedWebView(true)
#if __ANDROID__
                                         .WithParentActivityOrWindow(CurrentActivity.Activity)
#endif
                                         .ExecuteAsync();
            }
            catch(MsalException msal)
            {
                if (msal.ErrorCode != MsalError.AuthenticationCanceledError && msal.ErrorCode != MsalError.PasswordRequiredForManagedUserError)
                {
                    LogException(msal, "LoginAsync", policy);
                }

                throw;
            }
            catch (Exception ex)
            {
                LogException(ex, "LoginAsync", policy);
                throw;
            }

            return result;
        }

        public Task<AuthenticationResult> SilentLoginAsync() => SilentLoginAsync(Options.DefaultPolicy);

        public async Task<AuthenticationResult> SilentLoginAsync(string policy)
        {
            AuthenticationResult result = null;

            try
            {
                var accounts = await AuthClient.GetAccountsAsync();

                result = await AuthClient.AcquireTokenSilent(Options.Scopes, accounts.FirstOrDefault())
                                         .WithB2CAuthority(Options.GetB2CAuthority(policy))
                                         .WithForceRefresh(true)
                                         .ExecuteAsync();
            }
            catch (Exception ex)
            {
                LogException(ex, "SilentLoginAsync", policy);
            }

            return result;
        }

        public async Task<bool> IsLoggedInAsync()
        {
            var result = await SilentLoginAsync();

            return result != null;
        }

        public async Task LogoutAsync()
        {
            foreach (var account in await AuthClient.GetAccountsAsync())
            {
                await AuthClient.RemoveAsync(account);
            }
        }

        public Task<IAccount> GetAccountAsync(string identifier) => 
            AuthClient.GetAccountAsync(identifier);

        private void LogException(Exception ex, string callingMethod, string policy)
        {
            if(ex is MsalException msal)
            {
                Logger.Report(msal, msal.GetExceptionInfo(new Dictionary<string, string>
                {
                    { "AuthServiceType", GetType().FullName },
                    { "Method", callingMethod },
                    { "Policy", policy }
                }));
            }
            else
            {
                Logger.Report(ex, new Dictionary<string, string>
                {
                    { "AuthServiceType", GetType().FullName },
                    { "Method", callingMethod },
                    { "Policy", policy }
                });
                Console.WriteLine($"*** UNIDENTIFIED ERROR:\n {ex}");
            }
        }
    }
}
