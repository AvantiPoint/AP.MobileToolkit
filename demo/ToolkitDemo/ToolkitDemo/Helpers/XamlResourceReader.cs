using System.IO;

namespace ToolkitDemo.Helpers
{
    public class XamlResourceReader : IXamlResourceReader
    {
        public string ReadEmbeddedResource(string resourceName)
        {
            var assembly = GetType().Assembly;
            string assemblyName = assembly.GetName().Name;

            if (resourceName.EndsWith(".xaml") || resourceName.EndsWith(".xaml.cs"))
            {
                resourceName = assemblyName + ".Views." + resourceName;
            }
            if (resourceName.EndsWith("ViewModel.cs"))
            {
                resourceName = assemblyName + ".ViewModels." + resourceName;
            }

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
