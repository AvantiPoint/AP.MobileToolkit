using System;
using System.Collections.Generic;

namespace AP.CrossPlatform.Collections
{
    /// <summary>
    /// IDictionary Extensions.
    /// </summary>
    public static class IDictionaryExtensions
    {
        /// <summary>
        /// Adds or updates the value for a specified key.
        /// </summary>
        /// <returns><c>true</c>, if Key was updated or added, <c>false</c> otherwise.</returns>
        /// <param name="dict">Dict.</param>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <typeparam name="TKey">The 1st type parameter.</typeparam>
        /// <typeparam name="TValue">The 2nd type parameter.</typeparam>
        public static bool AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            bool updated = false;

            if (key == null || (typeof(TKey) == typeof(string) && string.IsNullOrWhiteSpace(key as string)))
            {
                return updated;
            }

            if (updated = (dict.ContainsKey(key) && !dict[key].Equals(value)) || !dict.ContainsKey(key))
            {
                dict[key] = value;
            }

            return updated;
        }

        /// <summary>
        /// Gets the stored value for a specified key, or a default value if the key is not set
        /// or the value is null
        /// </summary>
        /// <returns>The value or default.</returns>
        /// <param name="dict">Dict.</param>
        /// <param name="key">Key.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <typeparam name="TKey">The 1st type parameter.</typeparam>
        /// <typeparam name="TValue">The 2nd type parameter.</typeparam>
        /// <typeparam name="T">The 3rd type parameter.</typeparam>
        public static T GetValueOrDefault<TKey, TValue, T>(this IDictionary<TKey, TValue> dict, TKey key, T defaultValue)
        {
            if (dict.ContainsKey(key) && dict[key] != null)
                return (T)Convert.ChangeType(dict[key], typeof(T));

            return defaultValue;
        }

        /// <summary>
        /// Indicates if the Dictionary is Null or Empty
        /// </summary>
        /// <returns><c>true</c>, if the dictionary is null or empty, <c>false</c> otherwise.</returns>
        /// <param name="dict">Dict.</param>
        /// <typeparam name="TKey">The 1st type parameter.</typeparam>
        /// <typeparam name="TValue">The 2nd type parameter.</typeparam>
        public static bool IsNullOrEmpty<TKey, TValue>(this IDictionary<TKey, TValue> dict) =>
            dict == null || dict.Count == 0;
    }
}
