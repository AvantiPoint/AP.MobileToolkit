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

        public DelegateCommand ShowCodeBehind { get; }

        public DelegateCommand GetApiData { get; }

        public APIClientPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ConsoleLoggingService logger, IApiClient apiClient)
            : base(navigationService, pageDialogService, logger)
        {
            ApiClient = apiClient;
            ShowCodeBehind = new DelegateCommand(ExecuteShowCodeBehind);
            GetApiData = new DelegateCommand(GetAPIData);
        }

        async void ExecuteShowCodeBehind()
        {
            await NavigationService.NavigateAsync("APIClientCodePage");
        }

        async void GetAPIData()
        {
            var result = await ApiClient.GetAsync("https://jsonplaceholder.typicode.com/todos/1");
            APIResult = result.IsSuccessStatusCode;
        }
    }
}
