using System.IO;
using System.Reflection;
using AP.MobileToolkit.Pages;

namespace ToolkitDemo.Helpers
{
    public class XamlResourceReader : IXamlResourceReader
    {
        public string ReadEmbeddedResource(string resourceName)
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(CustomPage)).Assembly;
            Stream stream = assembly.GetManifestResourceStream(resourceName);

            string resourceContent = "No info to show";

            if (stream != null)
            {
                using (var reader = new StreamReader(stream))
                {
                    resourceContent = reader.ReadToEnd();
                }
            }

            return resourceContent;
        }
    }
}
