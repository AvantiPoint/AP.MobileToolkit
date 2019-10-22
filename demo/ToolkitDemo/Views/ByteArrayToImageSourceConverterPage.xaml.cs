using ToolkitDemo.Helpers;
using ToolkitDemo.Views;
using Xamarin.Forms;

[assembly: MenuItem("Byte Array To ImageSource", nameof(ByteArrayToImageSourceConverterPage), MenuGroup.Converters)]
namespace ToolkitDemo.Views
{
    public partial class ByteArrayToImageSourceConverterPage : ContentPage
    {
        public ByteArrayToImageSourceConverterPage()
        {
            InitializeComponent();
        }
    }
}
