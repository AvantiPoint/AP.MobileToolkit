using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AP.CrossPlatform.Http;
using AP.MobileToolkit.Authentication;
using Prism.Logging;

namespace AP.MobileToolkit.Http
{
    public abstract class ApiClientBase : IApiClient
    {
        protected IAuthenticationHandler AuthenticationHandler { get; }
        protected HttpClient Client { get; }
        protected ILogger Logger { get; }
        protected string InstallId { get; }

        public ApiClientBase(IApiClientOptions options, IAuthenticationHandler authenticationHandler, ILogger logger)
        {
            InstallId = options.InstallId;
            AuthenticationHandler = authenticationHandler;
            var authenticationMessageHandler = new AuthenticationMessageHandler(authenticationHandler)
            {
                InnerHandler = new RetryRequestDelegateHandler()
            };
            Client = new HttpClient(authenticationMessageHandler)
            {
                BaseAddress = new Uri(options.BaseUri)
            };
            Logger = logger;
        }

        public virtual Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Client.GetAsync(requestUri, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> DeleteAsync(string requestUri, object content = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Client.DeleteAsync(requestUri, content, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> PostAsync(string requestUri, object content = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Client.PostJsonObjectAsync(requestUri, content, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> PutAsync(string requestUri, object content = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Client.PutAsync(requestUri, content, cancellationToken);
        }

        public virtual async Task<IUser> GetUserAsync()
        {
            try
            {
                var token = await AuthenticationHandler.GetTokenAsync().ConfigureAwait(false);
                return new JwtUser(token);
            }
            catch (Exception ex)
            {
                Logger.Report(ex);
                return null;
            }
        }
    }
}
