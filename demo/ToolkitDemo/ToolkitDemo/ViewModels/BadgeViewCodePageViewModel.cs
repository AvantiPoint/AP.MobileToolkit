using ToolkitDemo.Helpers;

namespace ToolkitDemo.ViewModels
{
    public class BadgeViewCodePageViewModel : CodePageViewModelBase
    {
        public BadgeViewCodePageViewModel(IXamlResourceReader xamlResourceReader, ICopyTextHelper copyTextHelper)
            : base(xamlResourceReader, copyTextHelper)
        {
            ResourceContent = _xamlResourceReader.ReadEmbeddedResource("AP.MobileToolkit.Controls.BadgeView.xaml");
        }
    }
}
