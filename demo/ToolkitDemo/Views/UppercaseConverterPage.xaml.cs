using ToolkitDemo.Helpers;
using ToolkitDemo.Views;
using Xamarin.Forms;

[assembly: MenuItem("Uppercase Converter", nameof(UppercaseConverterPage), MenuGroup.Converters)]
namespace ToolkitDemo.Views
{
    public partial class UppercaseConverterPage : ContentPage
    {
        public UppercaseConverterPage()
        {
            InitializeComponent();
        }
    }
}
