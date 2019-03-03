using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AP.MobileToolkit.Xaml
{
    public abstract class AssemblyAwareExtension<T> : IMarkupExtension<T>
    {
        public string AssemblyName { get; set; }

        public T ProvideValue(IServiceProvider serviceProvider)
        {
            Assembly assembly = null;

            if (string.IsNullOrWhiteSpace(AssemblyName))
            {
                IProvideValueTarget provideValueTarget = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
                var page = GetPage(provideValueTarget.TargetObject);
                assembly = page.GetType().Assembly;
            }
            else
            {
                assembly = Assembly.LoadFrom(AssemblyName);
            }

            return ProvideValue(serviceProvider, assembly);
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) => ProvideValue(serviceProvider);

        protected abstract T ProvideValue(IServiceProvider serviceProvider, Assembly assembly);

        protected object GetPage(object value)
        {
            switch (value)
            {
                case Page page:
                    return page;
                case Element element:
                    return GetPage(element.Parent);
                default:
                    return null;
            }
        }
    }
}
