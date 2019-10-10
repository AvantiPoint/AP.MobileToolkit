using Prism.Logging;
using Prism.Navigation;
using Prism.Services;

namespace ToolkitDemo.ViewModels
{
    public class UppercaseConverterPageViewModel : DemoPageViewModelBase
    {
        public UppercaseConverterPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
            : base(navigationService, pageDialogService, logger)
        {
            TextToBeConverted = "Text to be converted to uppercase";
        }

        public string TextToBeConverted { get; }
    }
}
