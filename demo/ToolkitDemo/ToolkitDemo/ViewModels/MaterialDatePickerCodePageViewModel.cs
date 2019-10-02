using ToolkitDemo.Helpers;

namespace ToolkitDemo.ViewModels
{
    public class MaterialDatePickerCodePageViewModel : CodePageViewModelBase
    {
        public MaterialDatePickerCodePageViewModel(IXamlResourceReader xamlResourceReader, ICopyTextHelper copyTextHelper)
            : base(xamlResourceReader, copyTextHelper)
        {
            ResourceContent = _xamlResourceReader.ReadEmbeddedResource("ToolkitDemo.Views.MaterialDatepickerPage.xaml");
        }
    }
}
