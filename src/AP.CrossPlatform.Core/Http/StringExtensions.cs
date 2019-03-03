using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using static System.Net.WebUtility;

namespace AP.CrossPlatform.Http
{
    /// <summary>
    /// String Extensions.
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// Adds the query string parameters.
        /// </summary>
        /// <returns>The query string parameters.</returns>
        /// <param name="uri">URI.</param>
        /// <param name="queryObject">Query object.</param>
        public static string AddQueryStringParameters(this string uri, object queryObject)
        {
            if (queryObject == null) return uri;

            Regex.Replace(uri, @"\/$", "");

            var prop = queryObject.GetType().GetRuntimeProperties()
                       // Only check Properties with the JsonPropertyAttribute
                       .Where(p => p.GetCustomAttributes().Any(attr => attr.GetType() == typeof(JsonPropertyAttribute)))
                       // Only get Properties that aren't Null. And IF they are a string make sure it isn't an empty string or white space.
                       .Where(p => p.GetValue(queryObject, null) != null || (p.GetType() == typeof(string) && !string.IsNullOrWhiteSpace(p.GetValue(queryObject, null).ToString())))
                       .Select(p => $"{p.GetCustomAttribute<JsonPropertyAttribute>().PropertyName}={UrlEncode(p.GetValue(queryObject, null).ToString())}");

            return $"{uri}/?{string.Join("&", prop)}";
        }

        /// <summary>
        /// Gets the URI hostname.
        /// </summary>
        /// <returns>The URI hostname.</returns>
        /// <param name="uriString">URI string.</param>
        public static string GetUriHostname(this string uriString)
        {
            return new Uri(uriString).Host;
        }

        /// <summary>
        /// Gets the URI port.
        /// </summary>
        /// <returns>The URI port.</returns>
        /// <param name="uriString">URI string.</param>
        public static int GetUriPort(this string uriString)
        {
            return new Uri(uriString).Port;
        }
    }
}
