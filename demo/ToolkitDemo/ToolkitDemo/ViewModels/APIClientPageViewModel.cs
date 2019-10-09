using AP.MobileToolkit.Http;
using AP.MobileToolkit.Mvvm;
using Prism.Commands;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;

namespace ToolkitDemo.ViewModels
{
    public class APIClientPageViewModel : ReactiveViewModelBase
    {
        private IApiClient ApiClient { get; }
        private bool _apiResult;
        public bool APIResult
        {
            get => _apiResult;
            set => _apiResult = value;
        }

        public DelegateCommand GetApiData { get; }

        public APIClientPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
            : base(navigationService, pageDialogService, logger)
        {
            GetApiData = new DelegateCommand(GetAPIData);
        }

        private void GetAPIData()
        {
        }
    }
}
