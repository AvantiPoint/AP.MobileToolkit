using System.Collections.Generic;

namespace ToolkitDemo.Helpers
{
    public class PageNameHelper : IPageNameHelper
    {
        public IEnumerable<string> GetPageFilesName(string pageName)
        {
            string xamlFileName = string.Concat(pageName, ".xaml");
            string xamlClassName = string.Concat(pageName, ".xaml.cs");
            string viewModelClassName = string.Concat(pageName, "ViewModel.cs");
            return new List<string>() { xamlFileName, xamlClassName, viewModelClassName };
        }
    }
}
