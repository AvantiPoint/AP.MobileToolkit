using Prism.Commands;
using Prism.Mvvm;
using ToolkitDemo.Helpers;

namespace ToolkitDemo.ViewModels
{
    public class CodePageViewModelBase : BindableBase
    {
        protected IXamlResourceReader _xamlResourceReader;
        private ICopyTextHelper _copyTextHelper;
        public string ResourceContent { get; set; }

        private DelegateCommand _copyTextToClipboard;
        public DelegateCommand CopyTextToClipboard =>
            _copyTextToClipboard ?? (_copyTextToClipboard = new DelegateCommand(CopyText));

        public CodePageViewModelBase(IXamlResourceReader xamlResourceReader, ICopyTextHelper copyTextHelper)
        {
            _xamlResourceReader = xamlResourceReader;
            _copyTextHelper = copyTextHelper;
        }

        private void CopyText()
        {
            _copyTextHelper.CopyToClipboard(ResourceContent);
        }
    }
}
