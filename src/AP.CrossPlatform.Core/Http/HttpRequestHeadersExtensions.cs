using System.Net.Http.Headers;

namespace AP.CrossPlatform.Http
{
    /// <summary>
    /// Http request headers extensions.
    /// </summary>
    public static class HttpRequestHeadersExtensions
    {
        /// <summary>
        /// Add the specified headers, name and value.
        /// </summary>
        /// <returns>The add.</returns>
        /// <param name="headers">Headers.</param>
        /// <param name="name">Name.</param>
        /// <param name="value">Value.</param>
        public static void Add(this HttpRequestHeaders headers, string name, object value)
        {
            headers.Add(name, $"{value}");
        }
    }
}
