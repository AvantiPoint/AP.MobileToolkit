using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace ToolkitDemo.ViewModels
{
    public class DoubleRoundingConverterPageViewModel : DemoPageViewModelBase
    {
        public DoubleRoundingConverterPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
            : base(navigationService, pageDialogService, logger)
        {
            Value = 3.75458;
        }

        public double Value
        {
            get => _value;
            set => this.RaiseAndSetIfChanged(ref _value, value);
        }

        private double _value;
    }
}
