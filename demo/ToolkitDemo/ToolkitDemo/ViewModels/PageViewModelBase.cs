using System.Reflection;
using AP.MobileToolkit.Mvvm;
using Prism.Commands;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace ToolkitDemo.ViewModels
{
    public class PageViewModelBase : ReactiveViewModelBase
    {
        public ImageSource Logo { get; private set; }

        private DelegateCommand _showCodeBehind;
        public DelegateCommand ShowCodeBehind => _showCodeBehind ?? (_showCodeBehind = new DelegateCommand(ExecuteShowCodeBehind));

        public PageViewModelBase(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
            : base(navigationService, pageDialogService, logger)
        {
            Logo = ImageSource.FromResource("ToolkitDemo.Images.logo.png", Assembly.GetExecutingAssembly());
        }

        public async void ExecuteShowCodeBehind()
        {
            string codePage = GetType().Name.Replace("PageViewModel", "CodePage");
            await NavigationService.NavigateAsync(codePage);
        }
    }
}
