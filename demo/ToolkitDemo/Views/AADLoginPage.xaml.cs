using ToolkitDemo.Helpers;
using ToolkitDemo.Views;
using Xamarin.Forms;

[assembly: MenuItem("Azure AD Authentication", nameof(AADLoginPage), MenuGroup.AzureADAuth)]
namespace ToolkitDemo.Views
{
    public partial class AADLoginPage : ContentPage
    {
        public AADLoginPage()
        {
            InitializeComponent();
        }
    }
}
