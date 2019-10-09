using System.Collections.Generic;

namespace ToolkitDemo.Services
{
    public interface ICodeSampleResolver
    {
        IEnumerable<string> GetPageFilesName(string pageName);
        string ReadEmbeddedResource(string resourceName);
    }
}
