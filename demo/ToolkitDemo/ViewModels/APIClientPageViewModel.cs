using AP.MobileToolkit.Http;
using AP.MobileToolkit.Mvvm;
using Prism.Commands;

namespace ToolkitDemo.ViewModels
{
    public class APIClientPageViewModel : APBaseViewModel
    {
        private IApiClient ApiClient { get; }

        public APIClientPageViewModel(BaseServices baseServices)
            : base(baseServices)
        {
            GetApiData = new DelegateCommand(GetAPIData);
        }

        public bool APIResult
        {
            get => _apiResult;
            set => _apiResult = value;
        }
        private bool _apiResult;

        public DelegateCommand GetApiData { get; }

        private void GetAPIData()
        {
        }
    }
}
