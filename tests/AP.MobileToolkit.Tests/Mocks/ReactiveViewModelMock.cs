using AP.MobileToolkit.Mvvm;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;

namespace AP.MobileToolkit.Tests.Mocks
{
    public class ReactiveViewModelMock : ReactiveViewModelBase
    {
        public ReactiveViewModelMock() : this(null, null, null) { }

        public ReactiveViewModelMock(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger) 
            : base(navigationService, pageDialogService, logger)
        {

        }

        protected override ObservableAsPropertyHelper<bool> GetIsBusyProperty()
        {
            ToggleIsBusyCommand = ReactiveCommand.Create(() => IsToggled = !IsToggled);
            return this.WhenAnyValue(x => x.IsToggled)
                .ToProperty(this, x => x.IsBusy, false);
        }

        private bool _isToggled;
        public bool IsToggled
        {
            get => _isToggled;
            set => this.RaiseAndSetIfChanged(ref _isToggled, value);
        }

        public ReactiveCommand<Unit, bool> ToggleIsBusyCommand { get; private set; }
    }
}
