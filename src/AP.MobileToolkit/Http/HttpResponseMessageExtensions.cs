using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AP.MobileToolkit.Http
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> ConvertAsync<T>(this HttpResponseMessage message)
        {
            try
            {
                var content = await message.Content.ReadAsStringAsync();
                if (message.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(content);
                }

                throw new ApiClientException(TryGetErrorMessage(content), message);
            }
            catch(ApiClientException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApiClientException(message.ReasonPhrase, message, ex);
            }
        }

        public static async Task EnsureValidRequestAsync(this HttpResponseMessage message)
        {
            if (!message.IsSuccessStatusCode)
            {
                var content = await message.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(content))
                {
                    content = message.ReasonPhrase;
                }

                throw new ApiClientException(content, message);
            }
        }

        private static string TryGetErrorMessage(string rawContent)
        {
            try
            {
                var errorType = new { Error = "" };
                return JsonConvert.DeserializeAnonymousType(rawContent, errorType).Error;
            }
            catch
            {
                return rawContent;
            }
        }
    }
}
