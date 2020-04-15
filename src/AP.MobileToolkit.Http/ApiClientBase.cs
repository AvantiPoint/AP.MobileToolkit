using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using AP.CrossPlatform.Auth;
using AP.CrossPlatform.Http;
using Prism.Logging;

namespace AP.MobileToolkit.Http
{
    public abstract class ApiClientBase : IApiClient, IAuthenticationHandler
    {
        protected IApiClientOptions Options { get; }

        protected ILogger Logger { get; }

        protected string InstallId { get; }

        private HttpClient _client;

        private HttpClient Client
        {
            get
            {
                if (_client is null)
                {
                    _client = CreateClient();
                }

                return _client;
            }
        }

        public ApiClientBase(IApiClientOptions options, ILogger logger)
        {
            InstallId = options.InstallId;
            Options = options;
            Logger = logger;
        }

        public virtual Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken = default)
        {
            return Client.GetAsync(requestUri, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> DeleteAsync(string requestUri, object content = null, CancellationToken cancellationToken = default)
        {
            return Client.DeleteAsync(requestUri, content, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> PatchAsync(string requestUri, object content = null, CancellationToken cancellationToken = default)
        {
            return Client.PatchAsync(requestUri, content, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> PostAsync(string requestUri, object content = null, CancellationToken cancellationToken = default)
        {
            return Client.PostJsonObjectAsync(requestUri, content, cancellationToken);
        }

        public virtual Task<HttpResponseMessage> PutAsync(string requestUri, object content = null, CancellationToken cancellationToken = default)
        {
            return Client.PutAsync(requestUri, content, cancellationToken);
        }

        public virtual async Task<IUser> GetUserAsync()
        {
            try
            {
                var token = await GetTokenAsync().ConfigureAwait(false);
                return new JwtUser(token);
            }
            catch (Exception ex)
            {
                Logger.Report(ex);
                return null;
            }
        }

        protected virtual void SetDefaultHeaders(HttpClient client)
        {
        }

        protected virtual HttpMessageHandler CreateHandler(HttpMessageHandler innerHandler) => innerHandler;

        private HttpClient CreateClient()
        {
            var authenticationMessageHandler = new AuthenticationMessageHandler(this)
            {
                InnerHandler = new RetryRequestDelegateHandler()
            };

            var handler = CreateHandler(authenticationMessageHandler);
            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri(Options.BaseUri)
            };

            SetDefaultHeaders(client);
            disposedValue = false;

            return client;
        }

        protected virtual Task<string> GetTokenAsync()
        {
            Logger.Warn("You must override ApiClientBase.GetTokenAsync() to make authenticated calls.");
            throw new NotImplementedException();
        }

        protected virtual void SetAuthenticationHeader(HttpRequestMessage request, string token)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("BEARER", token);
        }

        Task<string> IAuthenticationHandler.GetTokenAsync() => GetTokenAsync();

        void IAuthenticationHandler.SetAuthenticationHeader(HttpRequestMessage request, string token) => SetAuthenticationHeader(request, token);

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _client?.Dispose();
                    _client = null;
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
