using System;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace AP.MobileToolkit.Xaml
{
    [ContentProperty(nameof(Name))]
    public class EmbeddedImageExtension : AssemblyAwareExtension<ImageSource>
    {
        public string Name { get; set; }

        protected override ImageSource ProvideValue(IServiceProvider serviceProvider, Assembly assembly)
        {
            var resourceName = GetResourceName(assembly);

            if (string.IsNullOrWhiteSpace(resourceName))
            {
                Console.WriteLine($"Could not locate a resource for '{Name}'");
                return default;
            }

            return ImageSource.FromStream(() => assembly.GetManifestResourceStream(resourceName));
        }

        internal string GetResourceName(Assembly assembly) =>
            assembly.GetManifestResourceNames().FirstOrDefault(n => n.EndsWith(Name, StringComparison.OrdinalIgnoreCase));
    }
}
