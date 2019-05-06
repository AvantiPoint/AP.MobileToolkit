using AP.MobileToolkit.Authentication;
using Microsoft.Identity.Client;

namespace AP.MobileToolkit.AAD
{
    public class MsalUser : JwtUser
    {
        public MsalUser() { }

        public MsalUser(string jwt) : base(jwt) { }

        public MsalUser(AuthenticationResult result) : base(result.AccessToken) { }
    }
}
