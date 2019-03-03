using AP.MobileToolkit.Authentication;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace AP.MobileToolkit.AAD
{
    public class MsalUser : JwtUser
    {
        public MsalUser() { }

        public MsalUser(string jwt) : base(jwt) { }

        public MsalUser(AuthenticationResult result) : base(result.AccessToken) { }
    }
}
