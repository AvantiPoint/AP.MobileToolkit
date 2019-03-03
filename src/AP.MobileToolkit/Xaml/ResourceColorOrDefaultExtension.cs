using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AP.MobileToolkit.Xaml
{
    [AcceptEmptyServiceProvider]
    [ContentProperty(nameof(Key))]
    public sealed class ResourceColorOrDefaultExtension : IMarkupExtension
    {
        public string Key { get; set; }

        public Color Default { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            var resources = Application.Current.Resources;

            if (resources.ContainsKey(Key) && resources[Key] is Color color)
            {
                return color;
            }
            else if (resources.ContainsKey($"{Key}Color") && resources[$"{Key}Color"] is Color explicitColor)
            {
                return explicitColor;
            }

            return Default;
        }
    }
}
