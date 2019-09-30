using AP.MobileToolkit.Mvvm;
using Prism.Commands;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;

namespace ToolkitDemo.ViewModels
{
    public class MaterialTimePickerPageViewModel : ReactiveViewModelBase
    {
        private DelegateCommand _showCodeBehind;
        public DelegateCommand ShowCodeBehind => _showCodeBehind ?? (_showCodeBehind = new DelegateCommand(ExecuteShowCodeBehind));

        public MaterialTimePickerPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
            : base(navigationService, pageDialogService, logger)
        {
        }

        private async void ExecuteShowCodeBehind()
        {
            await NavigationService.NavigateAsync("MaterialTimePickerCodePage");
        }
    }
}
