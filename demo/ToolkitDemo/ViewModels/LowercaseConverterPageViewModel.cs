using Prism.Logging;
using Prism.Navigation;
using Prism.Services;

namespace ToolkitDemo.ViewModels
{
    public class LowercaseConverterPageViewModel : DemoPageViewModelBase
    {
        public LowercaseConverterPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
            : base(navigationService, pageDialogService, logger)
        {
            TextToBeConverted = "TEXT TO BE CONVERTED TO LOWERCASE";
        }

        public string TextToBeConverted { get; }
    }
}
