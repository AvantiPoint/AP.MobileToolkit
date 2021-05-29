using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using AP.CrossPlatform.i18n;
using AP.MobileToolkit.Mvvm;
using Moq;
using Prism.Events;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using ReactiveUI;
using Xunit.Abstractions;

namespace AP.MobileToolkit.Tests.Mocks
{
    public class ReactivePageViewModel : ReactiveMockViewModel
    {
        public ReactivePageViewModel(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }
    }

    public class ReactiveViewModelMock : ReactiveMockViewModel
    {
        public ReactiveViewModelMock(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }
    }

    public class ReactiveMockViewModel : APBaseViewModel
    {
        public ReactiveMockViewModel(ITestOutputHelper testOutputHelper)
            : this(new BaseServices(Mock.Of<INavigationService>(), Mock.Of<IDialogService>(), Mock.Of<IPageDialogService>(), new XunitLogger(testOutputHelper), Mock.Of<IEventAggregator>(), Mock.Of<IDeviceService>(), new ResxLocalize()))
        {
        }

        public ReactiveMockViewModel(BaseServices baseServices)
            : base(baseServices)
        {
            ToggleIsBusyCommand = ReactiveCommand.Create(
                () => { IsToggled = !IsToggled; },
                this.WhenAnyValue(x => x.IsBusy)
                .Select(x => !x));
            _isBusyHelper = this.WhenAnyValue(x => x.IsToggled)
                .ToProperty(this, nameof(IsBusy), false, true)
                .DisposeWith(Disposables);
        }

        private bool _isToggled;

        public bool IsToggled
        {
            get => _isToggled;
            set => this.RaiseAndSetIfChanged(ref _isToggled, value);
        }

        public ReactiveCommand<Unit, Unit> ToggleIsBusyCommand { get; private set; }

        public string CorrelationId { get; private set; }

        protected override Task DisplayAlertForException(Exception ex, string correlationId)
        {
            CorrelationId = correlationId;
            return base.DisplayAlertForException(ex, correlationId);
        }
    }
}
