using ToolkitDemo.Helpers;

namespace ToolkitDemo.ViewModels
{
    public class MaterialEntryCodePageViewModel : CodePageViewModelBase
    {
        public MaterialEntryCodePageViewModel(IXamlResourceReader xamlResourceReader, ICopyTextHelper copyTextHelper)
            : base(xamlResourceReader, copyTextHelper)
        {
            ResourceContent = _xamlResourceReader.ReadEmbeddedResource("AP.MobileToolkit.Controls.MaterialEntry.xaml");
        }
    }
}
