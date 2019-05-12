using AP.MobileToolkit.Mvvm;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;

namespace AP.MobileToolkit.Tests.Mocks
{
    public class MockViewModel : ViewModelBase
    {
        public MockViewModel()
            : this(null, null, null)
        {
        }

        public MockViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
            : base(navigationService, pageDialogService, logger)
        {
        }
    }
}
