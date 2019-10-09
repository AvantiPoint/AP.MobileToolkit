using Prism.Logging;
using Prism.Navigation;
using Prism.Services;

namespace ToolkitDemo.ViewModels
{
    public class RadioButtonPageViewModel : DemoPageViewModelBase
    {
        public RadioButtonPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
            : base(navigationService, pageDialogService, logger)
        {
        }
    }
}
