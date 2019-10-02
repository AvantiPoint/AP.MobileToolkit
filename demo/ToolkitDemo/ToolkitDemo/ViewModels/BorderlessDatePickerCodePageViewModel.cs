using ToolkitDemo.Helpers;

namespace ToolkitDemo.ViewModels
{
    public class BorderlessDatePickerCodePageViewModel : CodePageViewModelBase
    {
        public BorderlessDatePickerCodePageViewModel(IXamlResourceReader xamlResourceReader, ICopyTextHelper copyTextHelper)
            : base(xamlResourceReader, copyTextHelper)
        {
            ResourceContent = _xamlResourceReader.ReadEmbeddedResource("ToolkitDemo.Views.BorderlessDatePickerPage.xaml");
        }
    }
}
