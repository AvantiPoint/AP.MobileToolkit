using Prism.Mvvm;
using ToolkitDemo.Helpers;

namespace ToolkitDemo.ViewModels
{
    public class BorderlessEntryCodePageViewModel : BindableBase
    {
        public IXamlResourceReader XamlResourceReader { get; set; }
        public string ResourceContent { get; set; }

        public BorderlessEntryCodePageViewModel(IXamlResourceReader xamlResourceReader)
        {
            XamlResourceReader = xamlResourceReader;
            ResourceContent = XamlResourceReader.ReadEmbeddedResource("AP.MobileToolkit.Controls.DatePickerCell.xaml");
        }
    }
}
