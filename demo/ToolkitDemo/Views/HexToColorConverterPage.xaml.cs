using ToolkitDemo.Helpers;
using ToolkitDemo.Views;
using Xamarin.Forms;

[assembly: MenuItem("Hex To Color", nameof(HexToColorConverterPage), MenuGroup.Converters)]
namespace ToolkitDemo.Views
{
    public partial class HexToColorConverterPage : ContentPage
    {
        public HexToColorConverterPage()
        {
            InitializeComponent();
        }
    }
}
