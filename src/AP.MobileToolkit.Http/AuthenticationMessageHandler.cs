using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AP.MobileToolkit.Http
{
    internal class AuthenticationMessageHandler : DelegatingHandler
    {
        private IAuthenticationHandler AuthenticationHandler { get; }

        public AuthenticationMessageHandler(IAuthenticationHandler authenticationHandler)
            : this(authenticationHandler, new HttpClientHandler())
        {
        }

        public AuthenticationMessageHandler(IAuthenticationHandler authenticationHandler, HttpClientHandler internalHandler)
            : base(internalHandler)
        {
            AuthenticationHandler = authenticationHandler;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var stackTrace = new StackTrace();
            var frames = stackTrace.GetFrames();
            var callingMethod = (from f in frames
                                 where FindMethod(f.GetMethod())
                                 select f.GetMethod()).FirstOrDefault();

            if (callingMethod.GetCustomAttribute<AllowAnonymousAttribute>() == null)
            {
                var token = await AuthenticationHandler.GetTokenAsync().ConfigureAwait(false);
                AuthenticationHandler.SetAuthenticationHeader(request, token);
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        private bool FindMethod(MethodBase method)
        {
            var declaringType = method.DeclaringType;

            // Should not be part of the MobileToolkit
            if (declaringType.Assembly == GetType().Assembly)
                return false;

            // Should not be part of the CLR or from Microsoft
            if (declaringType.FullName.StartsWith("System") || declaringType.FullName.StartsWith("Microsoft"))
                return false;

            // Should not be part of Polly
            if (declaringType.FullName.StartsWith("Polly"))
                return false;

            // Should be an actual type
            if (declaringType.FullName.Contains("+"))
                return false;

            return true;
        }
    }
}
