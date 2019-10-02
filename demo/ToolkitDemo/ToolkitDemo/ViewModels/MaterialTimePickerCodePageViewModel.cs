using ToolkitDemo.Helpers;

namespace ToolkitDemo.ViewModels
{
    public class MaterialTimePickerCodePageViewModel : CodePageViewModelBase
    {
        public MaterialTimePickerCodePageViewModel(IXamlResourceReader xamlResourceReader, ICopyTextHelper copyTextHelper)
            : base(xamlResourceReader, copyTextHelper)
        {
            ResourceContent = _xamlResourceReader.ReadEmbeddedResource("ToolkitDemo.Views.MaterialTimePickerPage.xaml");
        }
    }
}
