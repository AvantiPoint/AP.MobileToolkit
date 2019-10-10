using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ToolkitDemo.Services
{
    public class CodeSampleResolver : ICodeSampleResolver
    {
        public IEnumerable<string> GetPageFilesName(string pageName)
        {
            return new[]
            {
                $"{pageName}.xaml",
                $"{pageName}.xaml.cs",
                $"{pageName}ViewModel.cs"
            };
        }

        public string ReadEmbeddedResource(string resourceName)
        {
            var assembly = GetType().Assembly;
            string fullResourceName = assembly.GetManifestResourceNames().FirstOrDefault(x => x.ToLower().EndsWith(resourceName.ToLower()));
            string resourceContent = "No info to show";
            Stream stream = assembly.GetManifestResourceStream(fullResourceName);

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
