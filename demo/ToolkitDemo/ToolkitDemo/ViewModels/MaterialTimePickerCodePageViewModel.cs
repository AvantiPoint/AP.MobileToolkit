using Prism.Commands;
using Prism.Mvvm;
using ToolkitDemo.Helpers;

namespace ToolkitDemo.ViewModels
{
    public class MaterialTimePickerCodePageViewModel : BindableBase
    {
        public IXamlResourceReader XamlResourceReader { get; set; }
        public ICopyTextHelper CopyTextHelper { get; set; }
        public string ResourceContent { get; set; }
        private DelegateCommand _copyTextToClipboard;
        public DelegateCommand CopyTextToClipboard =>
            _copyTextToClipboard ?? (_copyTextToClipboard = new DelegateCommand(CopyText));

        public MaterialTimePickerCodePageViewModel(IXamlResourceReader xamlResourceReader, ICopyTextHelper copyTextHelper)
        {
            XamlResourceReader = xamlResourceReader;
            CopyTextHelper = copyTextHelper;
            ResourceContent = XamlResourceReader.ReadEmbeddedResource("AP.MobileToolkit.Controls.MaterialTimePicker.xaml");
        }
        void CopyText()
        {
            CopyTextHelper.CopyToClipboard(ResourceContent);
        }
    }
}
