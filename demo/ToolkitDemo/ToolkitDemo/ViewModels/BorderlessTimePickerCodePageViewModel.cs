using ToolkitDemo.Helpers;

namespace ToolkitDemo.ViewModels
{
    public class BorderlessTimePickerCodePageViewModel : CodePageViewModelBase
    {
        public BorderlessTimePickerCodePageViewModel(IXamlResourceReader xamlResourceReader, ICopyTextHelper copyTextHelper)
             : base(xamlResourceReader, copyTextHelper)
        {
            ResourceContent = _xamlResourceReader.ReadEmbeddedResource("ToolkitDemo.Views.BorderlessTimePickerPage.xaml");
        }
    }
}
