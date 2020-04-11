using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace AP.MobileToolkit.AAD
{
    public sealed class AuthenticationService : IAuthenticationService
    {
        private IPublicClientApplication AuthClient { get; }

        private IAuthConfiguration Config { get; }

        private readonly Subject<string> _accessToken;

        private readonly Subject<AuthenticationResult> _authResult;

        public AuthenticationService(IAuthConfiguration config, IPublicClientApplication publicClientApplication)
        {
            _accessToken = new Subject<string>();
            _authResult = new Subject<AuthenticationResult>();
            Config = config;
            AuthClient = publicClientApplication;
        }

        public IObservable<string> AccessToken => _accessToken;

        public IObservable<AuthenticationResult> AuthenticationResult => _authResult;

        public Task<IEnumerable<IAccount>> GetAccountsAsync() => AuthClient.GetAccountsAsync();

        public Task<IMsalResult> LoginAsync() => LoginAsync(Config.Policy);

        public async Task<IMsalResult> LoginAsync(string policy)
        {
            try
            {
                var result = await SilentLoginAsync(policy);

                if (result.Success)
                    return result;

                var builder = AuthClient.AcquireTokenInteractive(Config.Scopes)
#if __ANDROID__
                                         .WithParentActivityOrWindow(Xamarin.Essentials.Platform.CurrentActivity)
#endif

                             .WithUseEmbeddedWebView(true);
                if (Config.IsB2C)
                {
                    builder = builder.WithB2CAuthority(Config.GetB2CAuthority(policy));
                }

                var authResult = await builder.ExecuteAsync();
                _authResult.OnNext(authResult);
                _accessToken.OnNext(authResult.AccessToken);
                return new MsalResult(authResult);
            }
            catch (Exception ex)
            {
                return new MsalResult(ex);
            }
        }

        public Task<IMsalResult> SilentLoginAsync() => SilentLoginAsync(Config.Policy);

        public async Task<IMsalResult> SilentLoginAsync(string policy)
        {
            try
            {
                var accounts = await AuthClient.GetAccountsAsync();

                var builder = AuthClient.AcquireTokenSilent(Config.Scopes, accounts.FirstOrDefault());

                if (Config.IsB2C)
                {
                    builder.WithB2CAuthority(Config.GetB2CAuthority(policy));
                }

                var authResult = await builder.WithForceRefresh(true)
                                    .ExecuteAsync();

                _authResult.OnNext(authResult);
                _accessToken.OnNext(authResult.AccessToken);
                return new MsalResult(authResult);
            }
            catch (Exception ex)
            {
                return new MsalResult(ex);
            }
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

            _accessToken.OnNext(null);
            _authResult.OnNext(null);
        }

        public Task<IAccount> GetAccountAsync(string identifier) =>
            AuthClient.GetAccountAsync(identifier);

        public async Task<IMsalResult> EditProfileAsync(string policy)
        {
            if (!Config.IsB2C)
                throw new NotSupportedException("This function is only supported for Azure Active Directory B2C");

            try
            {
                var authResult = await AcquireTokenInteractive(policy);

                _authResult.OnNext(authResult);
                _accessToken.OnNext(authResult.AccessToken);
                return new MsalResult(authResult);
            }
            catch (Exception ex)
            {
                return new MsalResult(ex);
            }
        }

        public async Task<IMsalResult> PasswordResetAsync(string policy)
        {
            if (!Config.IsB2C)
                throw new NotSupportedException("This function is only supported for Azure Active Directory B2C");

            try
            {
                var authResult = await AcquireTokenInteractive(policy);

                _authResult.OnNext(authResult);
                _accessToken.OnNext(authResult.AccessToken);
                return new MsalResult(authResult);
            }
            catch (Exception ex)
            {
                return new MsalResult(ex);
            }
        }

        private async Task<AuthenticationResult> AcquireTokenInteractive(string policy)
        {
            IEnumerable<IAccount> accounts = await AuthClient.GetAccountsAsync();
            var account = GetAccountByPolicy(accounts, policy);
            var builder = AuthClient.AcquireTokenInteractive(Config.Scopes)
                                    .WithAccount(account)
#if __ANDROID__
                                    .WithParentActivityOrWindow(Xamarin.Essentials.Platform.CurrentActivity)
#endif

                                    .WithUseEmbeddedWebView(true)
                                    .WithAuthority(Config.GetB2CAuthority(policy));

            return await builder.ExecuteAsync();
        }

        private IAccount GetAccountByPolicy(IEnumerable<IAccount> accounts, string policy)
        {
            foreach (var account in accounts)
            {
                string userIdentifier = account.HomeAccountId.ObjectId.Split('.')[0];
                if (userIdentifier.EndsWith(policy.ToLower()))
                    return account;
            }

            return null;
        }
    }
}
