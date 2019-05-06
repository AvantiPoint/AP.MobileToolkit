using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AP.MobileToolkit.Http
{
    public class ApiClientException : Exception
    {
        public ApiClientException(string message)
            : base(message)
        {
        }

        public ApiClientException(string message, HttpResponseMessage httpResponse)
            : this(message, httpResponse, null)
        {

        }

        public ApiClientException(string message, HttpResponseMessage httpResponse, Exception innerException)
            : base(message, innerException)
        {
            ResponseMessage = httpResponse;
        }

        public HttpResponseMessage ResponseMessage { get; }
    }
}
