using ToolkitDemo.Helpers;
using ToolkitDemo.Views;
using Xamarin.Forms;

[assembly: MenuItem("Lowercase Converter", nameof(LowercaseConverterPage), MenuGroup.Converters)]
namespace ToolkitDemo.Views
{
    public partial class LowercaseConverterPage : ContentPage
    {
        public LowercaseConverterPage()
        {
            InitializeComponent();
        }
    }
}
