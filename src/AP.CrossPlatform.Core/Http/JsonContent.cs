using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AP.CrossPlatform.Http
{
    /// <summary>
    /// Json Content.
    /// </summary>
    public class JsonContent : ByteArrayContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:AP.CrossPlatform.Http.JsonContent"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="encoding">Encoding.</param>
        public JsonContent(JObject message, Encoding encoding = null)
            : base(GetByteArray(message.ToString(), encoding))
        {
            Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json")
            {
                CharSet = (encoding ?? Encoding.UTF8).WebName
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:AP.CrossPlatform.Http.JsonContent"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="encoding">Encoding.</param>
        public JsonContent(object message, Encoding encoding = null)
            : base(GetByteArray(message, encoding))
        {
            Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json")
            {
                CharSet = (encoding ?? Encoding.UTF8).WebName
            };
        }

        static byte[] GetByteArray(object content, Encoding encoding = null)
        {
            return GetByteArray(JsonConvert.SerializeObject(content), encoding);
        }

        static byte[] GetByteArray(string content, Encoding encoding = null)
        {
            return (encoding ?? Encoding.UTF8).GetBytes(content);
        }
    }
}
