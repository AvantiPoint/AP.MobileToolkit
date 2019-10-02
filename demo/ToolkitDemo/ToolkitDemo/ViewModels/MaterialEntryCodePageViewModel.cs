using ToolkitDemo.Helpers;

namespace ToolkitDemo.ViewModels
{
    public class MaterialEntryCodePageViewModel : CodePageViewModelBase
    {
        public MaterialEntryCodePageViewModel(IXamlResourceReader xamlResourceReader, ICopyTextHelper copyTextHelper)
            : base(xamlResourceReader, copyTextHelper)
        {
            ResourceContent = _xamlResourceReader.ReadEmbeddedResource("ToolkitDemo.Views.MaterialEntryPage.xaml");
        }
    }
}
