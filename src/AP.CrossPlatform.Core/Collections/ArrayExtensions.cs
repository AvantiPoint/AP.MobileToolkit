using System;

namespace AP.CrossPlatform.Collections
{
    /// <summary>
    /// Array extensions.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Gets a Sub Array from a specified array
        /// </summary>
        /// <returns>The array.</returns>
        /// <param name="array">Array.</param>
        /// <param name="length">Length.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T[] SubArray<T>(this T[] array, int length)
        {
            if (length > array.Length)
                length -= length - array.Length;

            T[] result = new T[length];
            Array.Copy(array, result, length);
            return result;
        }
    }
}
