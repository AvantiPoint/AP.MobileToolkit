using AP.MobileToolkit.Mvvm;
using Prism.Commands;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;

namespace ToolkitDemo.ViewModels
{
    public class DemoPageViewModelBase : ReactiveViewModelBase
    {
        public DemoPageViewModelBase(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
            : base(navigationService, pageDialogService, logger)
        {
            PageName = GetType().Name.Replace("ViewModel", string.Empty);
            ShowCodeBehind = new DelegateCommand(ExecuteShowCodeBehind);
        }

        public string PageName { get; }

        public DelegateCommand ShowCodeBehind { get; }

        private async void ExecuteShowCodeBehind()
        {
            await NavigationService.NavigateAsync("ShowCodePage", ("page_name", PageName));
        }
    }
}
