using System;
using System.Linq;
using Foundation;

#pragma warning disable SA1300
namespace AP.MobileToolkit.Platform.iOS.Extensions
#pragma warning restore SA1300
{
    /// <summary>
    /// NSUser defaults extensions.
    /// </summary>
    public static class NSUserDefaultsExtensions
    {
        /// <summary>
        /// Adds the prefernce specifier observer.
        /// </summary>
        /// <param name="defaults">Defaults.</param>
        /// <param name="action">Action.</param>
        public static void AddPrefernceSpecifierObserver(this NSUserDefaults defaults, Action action)
        {
            defaults.AddObserver("PreferenceSpecifiers", NSKeyValueObservingOptions.New, (observedChange) =>
            {
                action?.Invoke();
            });
        }

        /// <summary>
        /// Gets the preference specifiers.
        /// </summary>
        /// <returns>The preference specifiers.</returns>
        /// <param name="defaults">Defaults.</param>
        public static NSDictionary[] GetPreferenceSpecifiers(this NSUserDefaults defaults)
        {
            return NSArray.FromArray<NSDictionary>(defaults["PreferenceSpecifiers"] as NSArray);
        }

        /// <summary>
        /// Gets the preference item.
        /// </summary>
        /// <returns>The preference item.</returns>
        /// <param name="defaults">Defaults.</param>
        /// <param name="key">Key.</param>
        public static NSDictionary GetPreferenceItem(this NSUserDefaults defaults, string key)
        {
            var preferenceItems = defaults.GetPreferenceSpecifiers();
            return preferenceItems?.FirstOrDefault(x => x["Key"]?.ToString() == key);
        }

        /// <summary>
        /// Gets the preference item default value.
        /// </summary>
        /// <returns>The preference item default value.</returns>
        /// <param name="defaults">Defaults.</param>
        /// <param name="key">Key.</param>
        public static NSObject GetPreferenceItemDefaultValue(this NSUserDefaults defaults, string key)
        {
            return defaults.GetPreferenceItem(key)?["DefaultValue"];
        }

        /// <summary>
        /// Gets the preference item default value.
        /// </summary>
        /// <returns>The preference item default value.</returns>
        /// <param name="preferenceItem">Preference item.</param>
        public static NSObject GetPreferenceItemDefaultValue(this NSDictionary preferenceItem)
        {
            return preferenceItem["DefaultValue"];
        }

        /// <summary>
        /// String from preference key.
        /// </summary>
        /// <returns>The from preference key.</returns>
        /// <param name="defaults">Defaults.</param>
        /// <param name="key">Key.</param>
        public static string StringFromPreferenceKey(this NSUserDefaults defaults, string key)
        {
            return defaults.GetPreferenceItemDefaultValue(key)?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// Int from preference key.
        /// </summary>
        /// <returns>The from preference key.</returns>
        /// <param name="defaults">Defaults.</param>
        /// <param name="key">Key.</param>
        /// <param name="defaultValue">Default value.</param>
        public static int IntFromPreferenceKey(this NSUserDefaults defaults, string key, int defaultValue = 0)
        {
            try
            {
                return int.Parse(defaults.GetPreferenceItemDefaultValue(key).ToString());
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return defaultValue;
            }
        }

        /// <summary>
        /// Gets the value as a specified type from preference key.
        /// </summary>
        /// <returns>The value from preference key.</returns>
        /// <param name="defaults">Defaults.</param>
        /// <param name="key">Key.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T GetValueFromPreferenceKey<T>(this NSUserDefaults defaults, string key, T defaultValue = default(T))
        {
            try
            {
                return (T)Convert.ChangeType(defaults.GetPreferenceItemDefaultValue(key).ToString(), typeof(T));
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}
