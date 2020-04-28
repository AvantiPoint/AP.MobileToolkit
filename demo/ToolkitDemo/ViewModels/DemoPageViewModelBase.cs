using AP.MobileToolkit.Mvvm;
using Prism.Commands;
using Prism.Navigation;

namespace ToolkitDemo.ViewModels
{
    public class DemoPageViewModelBase : APBaseViewModel
    {
        public DemoPageViewModelBase(BaseServices baseServices)
            : base(baseServices)
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
