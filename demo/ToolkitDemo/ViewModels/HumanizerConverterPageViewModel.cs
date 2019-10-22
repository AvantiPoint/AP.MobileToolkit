using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace ToolkitDemo.ViewModels
{
    public class HumanizerConverterPageViewModel : DemoPageViewModelBase
    {
        public HumanizerConverterPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
            : base(navigationService, pageDialogService, logger)
        {
            InputText = "This is sample text.";
        }

        public string InputText
        {
            get => _inputText;
            set => this.RaiseAndSetIfChanged(ref _inputText, value);
        }

        private string _inputText;
    }
}
