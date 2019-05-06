using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AP.MobileToolkit.Http
{
    internal class RetryRequestDelegateHandler : DelegatingHandler
    {
        public RetryRequestDelegateHandler() 
            : this(new HttpClientHandler())
        {

        }

        public RetryRequestDelegateHandler(HttpClientHandler internalHandler)
            : base(internalHandler)
        {

        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage result = null;
            for(int i = 0; i < 3; i++)
            {
                result = await base.SendAsync(request, cancellationToken);

                if(result.StatusCode == HttpStatusCode.RequestTimeout || 
                    result.StatusCode == HttpStatusCode.InternalServerError ||
                    result.StatusCode == HttpStatusCode.BadGateway ||
                    result.StatusCode == HttpStatusCode.ServiceUnavailable ||
                    result.StatusCode == HttpStatusCode.GatewayTimeout)
                {
                    await Task.Delay(250);
                    continue;
                }

                break;
            }
            return result;
        }
    }
}
