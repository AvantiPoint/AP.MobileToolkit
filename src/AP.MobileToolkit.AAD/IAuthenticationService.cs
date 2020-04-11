using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace AP.MobileToolkit.AAD
{
    public interface IAuthenticationService
    {
        IObservable<string> AccessToken { get; }

        IObservable<AuthenticationResult> AuthenticationResult { get; }

        Task<IMsalResult> LoginAsync();

        Task<IMsalResult> LoginAsync(string policy);

        Task<IMsalResult> SilentLoginAsync();

        Task<IMsalResult> SilentLoginAsync(string policy);

        Task<IMsalResult> EditProfileAsync(string policy);

        Task<IMsalResult> PasswordResetAsync(string policy);

        Task<bool> IsLoggedInAsync();

        Task LogoutAsync();

        Task<IEnumerable<IAccount>> GetAccountsAsync();

        Task<IAccount> GetAccountAsync(string identifier);
    }
}
