﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace AP.MobileToolkit.AAD
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> LoginAsync();

        Task<AuthenticationResult> LoginAsync(string policy);

        Task<AuthenticationResult> SilentLoginAsync();

        Task<AuthenticationResult> SilentLoginAsync(string policy);

        Task<bool> IsLoggedInAsync();

        Task LogoutAsync();

        Task<IEnumerable<IAccount>> GetAccountsAsync();

        Task<IAccount> GetAccountAsync(string identifier);
    }
}
