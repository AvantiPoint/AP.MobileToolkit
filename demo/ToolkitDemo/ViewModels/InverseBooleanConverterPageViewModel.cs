using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace ToolkitDemo.ViewModels
{
    public class InverseBooleanConverterPageViewModel : DemoPageViewModelBase
    {
        public InverseBooleanConverterPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
            : base(navigationService, pageDialogService, logger)
        {
        }

        public bool Value
        {
            get => _value;
            set => this.RaiseAndSetIfChanged(ref _value, value);
        }

        private bool _value;
    }
}
