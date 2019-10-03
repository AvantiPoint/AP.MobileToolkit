using System.Reflection;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace ToolkitDemo.ViewModels
{
    public class MaterialEntryPageViewModel : PageViewModelBase
    {
        public MaterialEntryPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
            : base(navigationService, pageDialogService, logger)
        {
        }
    }
}
