using System;
using AP.MobileToolkit.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AP.MobileToolkit.Xaml
{
    [ContentProperty(nameof(Email))]
    public class GravatarImageSourceExtension : GravatarImageSource, IMarkupExtension<GravatarImageSource>
    {
        public GravatarImageSource ProvideValue(IServiceProvider serviceProvider)
        {
            if (serviceProvider is null)
                throw new ArgumentNullException(nameof(serviceProvider));

            var valueTargetProvider = serviceProvider.GetService<IProvideValueTarget>();
            SetBinding(BindingContextProperty, new Binding("BindingContext", source: valueTargetProvider.TargetObject));

            return this;
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) =>
            ProvideValue(serviceProvider);
    }
}
