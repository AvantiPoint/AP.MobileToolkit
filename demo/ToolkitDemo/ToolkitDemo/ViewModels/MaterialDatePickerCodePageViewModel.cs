using ToolkitDemo.Helpers;

namespace ToolkitDemo.ViewModels
{
    public class MaterialDatePickerCodePageViewModel : CodePageViewModelBase
    {
        public MaterialDatePickerCodePageViewModel(IXamlResourceReader xamlResourceReader, ICopyTextHelper copyTextHelper)
            : base(xamlResourceReader, copyTextHelper)
        {
            ResourceContent = _xamlResourceReader.ReadEmbeddedResource("AP.MobileToolkit.Controls.MaterialDatePicker.xaml");
        }
    }
}
