using System.IO;
using AP.CrossPlatform.Collections;

namespace AP.CrossPlatform.IO
{
    /// <summary>
    /// Byte array extensions.
    /// </summary>
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Converts a byte array to a Stream
        /// </summary>
        /// <returns>The stream.</returns>
        /// <param name="data">Data.</param>
        public static Stream ToStream(this byte[] data)
        {
            if(data.IsNullOrEmpty()) return null;

            return new MemoryStream(data);
        }
    }
}