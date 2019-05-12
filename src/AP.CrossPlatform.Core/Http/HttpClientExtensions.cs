using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AP.CrossPlatform.Http
{
    /// <summary>
    /// Http Client extensions.
    /// </summary>
    public static class HttpClientExtensions
    {
        private static readonly HttpMethod PatchMethod = new HttpMethod("PATCH");

        private class ErrorResult
        {
            [JsonProperty("message")]
            public string Message { get; set; }
        }

        /// <summary>
        /// Gets the JObject Async.
        /// </summary>
        /// <returns>The JObject async.</returns>
        /// <param name="client">Client.</param>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="requestObject">Request object.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public static async Task<JObject> GetJObjectAsync(this HttpClient client, string requestUri, object requestObject = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var defaultResponse = "{\"message\":\"An unknown error has occured. Please try again. If this problem persists, please contact the closest Code Monkey.\"}";
                var response = await client.GetAsync(requestUri.AddQueryStringParameters(requestObject), cancellationToken).ConfigureAwait(false);
                string result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    return JObject.FromObject(new ErrorResult() { Message = $"{response.StatusCode} {result}" });
                }

                if (string.IsNullOrWhiteSpace(result))
                {
                    result = defaultResponse;
                }

                return JObject.Parse(result);
            }
            catch (Exception e)
            {
                return JObject.FromObject(new ErrorResult { Message = e.ToString() });
            }
        }

        /// <summary>
        /// Gets the specified type async
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="client">Client.</param>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="requestObject">Request object.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static async Task<T> GetAsync<T>(this HttpClient client, string requestUri, object requestObject = null, CancellationToken cancellationToken = default)
        {
            var response = await client.GetAsync(requestUri.AddQueryStringParameters(requestObject), cancellationToken).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Gets the specified type async
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="client">Client.</param>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="failedResponse">Failed response.</param>
        /// <param name="requestObject">Request object.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static async Task<T> GetAsync<T>(this HttpClient client, string requestUri, T failedResponse, object requestObject = null, CancellationToken cancellationToken = default)
        {
            var result = await client.GetAsync(requestUri.AddQueryStringParameters(requestObject), cancellationToken).ConfigureAwait(false);
            if (!result.IsSuccessStatusCode)
                return failedResponse;

            try
            {
                var jsonString = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch
            {
                return failedResponse;
            }
        }

        /// <summary>
        /// Gets the result of a specified key as a specified type async.
        /// </summary>
        /// <returns>The result async.</returns>
        /// <param name="client">Client.</param>
        /// <param name="resultKey">Result key.</param>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="requestObject">Request object.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static async Task<T> GetResultAsync<T>(this HttpClient client, string resultKey, string requestUri, object requestObject = null, CancellationToken cancellationToken = default)
        {
            try
            {
                return (T)Convert.ChangeType(await GetResultAsync(client, resultKey, requestUri, requestObject, cancellationToken).ConfigureAwait(false), typeof(T));
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// Gets the result of a specified key as a specified type async.
        /// </summary>
        /// <returns>The result async.</returns>
        /// <param name="client">Client.</param>
        /// <param name="resultKey">Result key.</param>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="requestObject">Request object.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public static async Task<string> GetResultAsync(this HttpClient client, string resultKey, string requestUri, object requestObject = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await client.GetJObjectAsync(requestUri, requestObject, cancellationToken).ConfigureAwait(false);
                return response[resultKey].ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Posts the Object as a Json Object async.
        /// </summary>
        /// <returns>The json object async.</returns>
        /// <param name="client">Client.</param>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="jsonObj">Json object.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public static async Task<HttpResponseMessage> PostJsonObjectAsync(this HttpClient client, string requestUri, object jsonObj, CancellationToken cancellationToken = default)
        {
            return await client.PostAsync(requestUri, new JsonContent(jsonObj), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the string async.
        /// </summary>
        /// <returns>The string async.</returns>
        /// <param name="client">Client.</param>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public static async Task<string> GetStringAsync(this HttpClient client, string requestUri, CancellationToken cancellationToken)
        {
            var response = await client.GetAsync(requestUri, cancellationToken).ConfigureAwait(false);
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        public static Task<HttpResponseMessage> PutAsync(this HttpClient client, string requestUri, object model, CancellationToken cancellationToken = default)
        {
            return client.SendAsync(new HttpRequestMessage(HttpMethod.Put, requestUri) { Content = new JsonContent(model) }, cancellationToken);
        }

        public static Task<HttpResponseMessage> DeleteAsync(this HttpClient client, string requestUri, object model, CancellationToken cancellationToken = default)
        {
            return client.SendAsync(new HttpRequestMessage(HttpMethod.Delete, requestUri) { Content = new JsonContent(model) }, cancellationToken);
        }

        /// <summary>
        /// Sends Http Path Async
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="client">Client.</param>
        /// <param name="address">Address.</param>
        /// <param name="content">Content.</param>
        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, string address, HttpContent content) =>
            await client.SendAsync(new HttpRequestMessage(PatchMethod, address) { Content = content }).ConfigureAwait(false);

        /// <summary>
        /// Sends Http Path Async
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="client">Client.</param>
        /// <param name="address">Address.</param>
        /// <param name="content">Content.</param>
        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, string address, string content) =>
            await client.SendAsync(new HttpRequestMessage(PatchMethod, address) { Content = new StringContent(content) }).ConfigureAwait(false);

        /// <summary>
        /// Sends Http Path Async
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="client">Client.</param>
        /// <param name="address">Address.</param>
        /// <param name="content">Content.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static async Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, string address, T content)
            where T : class =>
            await client.SendAsync(new HttpRequestMessage(PatchMethod, address) { Content = new JsonContent(content) }).ConfigureAwait(false);

        /// <summary>
        /// Sends Http Path Async
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="client">Client.</param>
        /// <param name="address">Address.</param>
        /// <param name="content">Content.</param>
        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, string address, JObject content) =>
            await client.SendAsync(new HttpRequestMessage(PatchMethod, address) { Content = new JsonContent(content) });
    }
}
