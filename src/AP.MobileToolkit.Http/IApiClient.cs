using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AP.CrossPlatform.Auth;

namespace AP.MobileToolkit.Http
{
    public interface IApiClient : IDisposable
    {
        Task<IUser> GetUserAsync();

        Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken = default);

        Task<HttpResponseMessage> DeleteAsync(string requestUri, object content = null, CancellationToken cancellationToken = default);

        Task<HttpResponseMessage> PatchAsync(string requestUri, object content = null, CancellationToken cancellationToken = default);

        Task<HttpResponseMessage> PostAsync(string requestUri, object content = null, CancellationToken cancellationToken = default);

        Task<HttpResponseMessage> PutAsync(string requestUri, object content = null, CancellationToken cancellationToken = default);
    }
}
