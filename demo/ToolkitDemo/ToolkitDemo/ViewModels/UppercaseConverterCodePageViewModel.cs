using ToolkitDemo.Helpers;

namespace ToolkitDemo.ViewModels
{
    public class UppercaseConverterCodePageViewModel : CodePageViewModelBase
    {
        public UppercaseConverterCodePageViewModel(IXamlResourceReader xamlResourceReader, ICopyTextHelper copyTextHelper)
            : base(xamlResourceReader, copyTextHelper)
        {
            ResourceContent = _xamlResourceReader.ReadEmbeddedResource("ToolkitDemo.Views.UppercaseConverterPage.xaml");
        }
    }
}
