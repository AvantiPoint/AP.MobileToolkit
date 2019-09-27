using Prism.Mvvm;
using ToolkitDemo.Helpers;

namespace ToolkitDemo.ViewModels
{
    public class MaterialDatePickerCodePageViewModel : BindableBase
    {
        public IXamlResourceReader XamlResourceReader { get; set; }
        public string ResourceContent { get; set; }

        public MaterialDatePickerCodePageViewModel(IXamlResourceReader xamlResourceReader)
        {
            XamlResourceReader = xamlResourceReader;
            ResourceContent = XamlResourceReader.ReadEmbeddedResource("AP.MobileToolkit.Controls.MaterialDatePicker.xaml");
        }
    }
}
