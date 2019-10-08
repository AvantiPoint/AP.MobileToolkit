using System.Collections.Generic;

namespace ToolkitDemo.Helpers
{
    public interface IPageNameHelper
    {
        IEnumerable<string> GetPageFilesName(string pageName);
    }
}
