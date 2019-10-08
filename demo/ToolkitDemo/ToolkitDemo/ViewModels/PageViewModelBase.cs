using AP.MobileToolkit.Mvvm;
using Prism.Commands;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;

namespace ToolkitDemo.ViewModels
{
    public class PageViewModelBase : ReactiveViewModelBase
    {
        public string PageName { get; set; }

        public DelegateCommand ShowCodeBehind { get; }

        private NavigationParameters navigationParams;

        public PageViewModelBase(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
            : base(navigationService, pageDialogService, logger)
        {
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
