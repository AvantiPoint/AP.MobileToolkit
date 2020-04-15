using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace AP.MobileToolkit.Http
{
    internal interface IAuthenticationHandler
    {
        Task<string> GetTokenAsync();
        void SetAuthenticationHeader(HttpRequestMessage request, string token);
    }
}
