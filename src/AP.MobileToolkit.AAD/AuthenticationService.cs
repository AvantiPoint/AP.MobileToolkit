using Microsoft.Identity.Client;
using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.MobileToolkit.AAD
{
    public class AuthenticationService : IAuthenticationService
    {
        private UIParent UiParent { get; }

        private IPublicClientApplication AuthClient { get; }

        private IAuthenticationOptions Options { get; }

        private ILogger Logger { get; }

        public AuthenticationService(IAuthenticationOptions authenticationOptions, ILogger logger, IPublicClientApplication publicClientApplication, UIParent parent)
        {
            Options = authenticationOptions;
            Logger = logger;
            AuthClient = publicClientApplication;
            UiParent = parent;
        }

        public Task<IEnumerable<IAccount>> GetAccountsAsync() => AuthClient.GetAccountsAsync();

        public virtual Task<AuthenticationResult> LoginAsync() => LoginAsync(Options.DefaultPolicy);

        public virtual async Task<AuthenticationResult> LoginAsync(string policy)
        {
            AuthenticationResult result = null;

            try
            {
                result = await SilentLoginAsync(policy);

                if (result != null)
                    return result;

                result = await AuthClient.AcquireTokenAsync(Options.Scopes, string.Empty, UIBehavior.Consent, string.Empty, Array.Empty<string>(), GetAuthority(policy), UiParent);
                SetResult(result);
            }
            catch (Exception ex)
            {
                if (ex is MsalException msal && msal.ErrorCode == MsalClientException.AuthenticationCanceledError)
                {
                    // The user cancelled the requested login.
                    throw msal;
                }
                LogException(ex, "LoginAsync", policy);
                throw;
            }

            return result;
        }

        public virtual Task<AuthenticationResult> SilentLoginAsync() => SilentLoginAsync(Options.DefaultPolicy);

        public virtual async Task<AuthenticationResult> SilentLoginAsync(string policy)
        {
            AuthenticationResult result = null;

            try
            {
                var accounts = await AuthClient.GetAccountsAsync();
                result = await AuthClient.AcquireTokenSilentAsync(Options.Scopes, accounts.FirstOrDefault(), GetAuthority(policy), false);
            }
            catch (Exception ex)
            {
                if (ex is MsalException msal && msal.ErrorCode == MsalClientException.AuthenticationCanceledError)
                {
                    // The user cancelled the requested login.
                    throw msal;
                }
                LogException(ex, "SilentLoginAsync", policy);
            }

            return result;
        }

        private string GetAuthority(string policy) => $"https://login.microsoftonline.com/tfp/{Options.Tenant}/{policy}";

        public virtual async Task<bool> IsLoggedInAsync()
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

        protected virtual void SetResult(AuthenticationResult result) { }

        protected void LogException(Exception ex, string callingMethod, string policy)
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

        protected string Base64UrlDecode(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
            var byteArray = Convert.FromBase64String(s);
            var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count());
            return decoded;
        }
    }
}
