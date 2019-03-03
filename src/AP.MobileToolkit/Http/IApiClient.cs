using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AP.MobileToolkit.Authentication;

namespace AP.MobileToolkit.Http
{
    public interface IApiClient
    {
        Task<IUser> GetUserAsync();
        Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken = default(CancellationToken));
        Task<HttpResponseMessage> DeleteAsync(string requestUri, object content = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<HttpResponseMessage> PostAsync(string requestUri, object content = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<HttpResponseMessage> PutAsync(string requestUri, object content = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
