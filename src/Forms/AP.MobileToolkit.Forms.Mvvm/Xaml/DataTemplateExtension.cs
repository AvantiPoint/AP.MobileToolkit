using System;
using Prism;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AP.MobileToolkit.Xaml
{
    [AcceptEmptyServiceProvider]
    [ContentProperty(nameof(Template))]
    public sealed class DataTemplateExtension : IMarkupExtension
    {
        public Type Type { get; set; }

        public DataTemplate Template { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Type != null)
            {
                if (Type.IsAssignableFrom(typeof(DataTemplate)))
                {
                    return PrismApplicationBase.Current.Container.Resolve(Type);
                }

                return new DataTemplate(Type);
            }

            return Template;
        }
    }
}
