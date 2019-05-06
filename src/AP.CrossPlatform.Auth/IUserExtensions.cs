using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AP.CrossPlatform.Auth
{
    public static class IUserExtensions
    {
        /// <summary>
        /// Gets the string value.
        /// </summary>
        /// <returns>The string value.</returns>
        /// <param name="key">Key.</param>
        /// <param name="defaultValue">Default value.</param>
        public static string GetStringValue(this IUser user, string key, string defaultValue = null)
        {
            if (!user.TryGetValue(key, out string r))
                r = defaultValue;
            return r;
        }

        public static IEnumerable<string> GetStringArrayValue(this IUser user, string key)
        {
            try
            {
                if(user.ContainsKey(key))
                {
                    var value = user[key];
                    if(Regex.IsMatch(value, @"\[.*\]"))
                        return JsonConvert.DeserializeObject<IEnumerable<string>>(value);

                    return new string[] { value.Trim() };
                }
            }
            catch(Exception ex)
            {
                return new string[] { ex.Message };
            }

            return Array.Empty<string>();
        }

        /// <summary>
        /// Sets the string value.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        public static void SetStringValue(this IUser user, string key, string value = null)
        {
            if (user.ContainsKey(key))
                user[key] = value;
            else
                user.Add(key, value);
        }

        /// <summary>
        /// Gets the date time value.
        /// </summary>
        /// <returns>The date time value.</returns>
        /// <param name="key">Key.</param>
        /// <param name="defaultValue">Default value.</param>
        public static DateTime? GetDateTimeValue(this IUser user, string key, DateTime? defaultValue = null)
        {

            var str = user.GetStringValue(key);

            if (string.IsNullOrEmpty(str))
                return defaultValue;

            if (long.TryParse(str, out long timestamp))
                return new DateTime(1970, 1, 1).AddSeconds(timestamp);

            if (DateTime.TryParse(str, out DateTime r))
                return r;

            return defaultValue;
        }

        /// <summary>
        /// Sets the date time value.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        public static void SetDateTimeValue(this IUser user, string key, DateTime? value = null)
        {
            if (!value.HasValue)
            {
                user.SetStringValue(key);
            }
            else
            {
                var t = value.Value.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                var s = (int)t.TotalSeconds;
                user.SetStringValue(key, s.ToString());
            }
        }

        /// <summary>
        /// Gets the long value.
        /// </summary>
        /// <returns>The long value.</returns>
        /// <param name="key">Key.</param>
        /// <param name="defaultValue">Default value.</param>
        public static long? GetLongValue(this IUser user, string key, long? defaultValue = null)
        {

            var str = user.GetStringValue(key);

            if (string.IsNullOrEmpty(str))
                return defaultValue;

            if (long.TryParse(str, out long r))
                return r;

            return defaultValue;
        }

        /// <summary>
        /// Sets the long value.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        public static void SetLongValue(this IUser user, string key, long? value = null)
        {
            if (!value.HasValue)
                user.SetStringValue(key);
            else
                user.SetStringValue(key, value.Value.ToString());
        }

        /// <summary>
        /// Gets the bool value.
        /// </summary>
        /// <returns>The bool value.</returns>
        /// <param name="key">Key.</param>
        /// <param name="defaultValue">Default value.</param>
        public static bool? GetBoolValue(this IUser user, string key, bool? defaultValue = null)
        {

            var str = user.GetStringValue(key);

            if (string.IsNullOrEmpty(str))
                return defaultValue;

            if(int.TryParse(str, out int i))
            {
                if (i == 0)
                    return false;
                else if (i == 1)
                    return true;
            }

            if (bool.TryParse(str, out bool r))
                return r;

            return defaultValue;
        }

        /// <summary>
        /// Sets the bool value.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        public static void SetBoolValue(this IUser user, string key, bool? value = null)
        {
            if (!value.HasValue)
                user.SetStringValue(key);
            else
                user.SetStringValue(key, value.Value.ToString());
        }
    }
}