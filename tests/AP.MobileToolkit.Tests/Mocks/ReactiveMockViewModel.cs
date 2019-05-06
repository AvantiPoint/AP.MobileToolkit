using AP.MobileToolkit.Mvvm;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using Moq;
using Xunit.Abstractions;
using System.Threading.Tasks;

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

    public class ReactiveMockViewModel : ReactiveViewModelBase
    {
        public ReactiveMockViewModel(ITestOutputHelper testOutputHelper)
            : base(Mock.Of<INavigationService>(), Mock.Of<IPageDialogService>(), new XunitLogger(testOutputHelper))
        {

        }

        public ReactiveMockViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger) 
            : base(navigationService, pageDialogService, logger)
        {
            
        }

        protected override ObservableAsPropertyHelper<bool> GetIsBusyProperty()
        {
            ToggleIsBusyCommand = ReactiveCommand.Create(() => { IsToggled = !IsToggled; },
                this.WhenAnyValue(x => x.IsBusy)
                .Select(x => !x));
            return this.WhenAnyValue(x => x.IsToggled)
                .ToProperty(this, x => x.IsBusy, false);
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
