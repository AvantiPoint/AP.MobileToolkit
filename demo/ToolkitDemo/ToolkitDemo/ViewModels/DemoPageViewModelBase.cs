using AP.MobileToolkit.Mvvm;
using Prism.Commands;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;

namespace ToolkitDemo.ViewModels
{
    public class DemoPageViewModelBase : ReactiveViewModelBase
    {
        public string PageName { get; }

        public DelegateCommand ShowCodeBehind { get; }

        private NavigationParameters navigationParams;

        public DemoPageViewModelBase(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
            : base(navigationService, pageDialogService, logger)
        {
            PageName = GetType().Name.Replace("ViewModel", string.Empty);
            ShowCodeBehind = new DelegateCommand(ExecuteShowCodeBehind);
        }

        public async void ExecuteShowCodeBehind()
        {
            navigationParams = new NavigationParameters();
            navigationParams.Add("page_name", PageName);
            await NavigationService.NavigateAsync("ShowCodePage", navigationParams);
        }
    }
}
