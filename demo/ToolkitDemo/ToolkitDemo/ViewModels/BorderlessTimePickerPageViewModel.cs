using Prism.Logging;
using Prism.Navigation;
using Prism.Services;

namespace ToolkitDemo.ViewModels
{
    public class BorderlessTimePickerPageViewModel : PageViewModelBase
    {
        public BorderlessTimePickerPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
            : base(navigationService, pageDialogService, logger)
        {
        }
    }
}
