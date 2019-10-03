using Prism.Logging;
using Prism.Navigation;
using Prism.Services;

namespace ToolkitDemo.ViewModels
{
    public class UppercaseConverterPageViewModel : PageViewModelBase
    {
        public string TextToBeConverted { get; set; }
        public UppercaseConverterPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
            : base(navigationService, pageDialogService, logger)
        {
            TextToBeConverted = "Text to be converted to uppercase";
        }
    }
}
