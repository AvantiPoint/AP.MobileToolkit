using AP.MobileToolkit.Extensions;
using AP.MobileToolkit.Resources;
using Prism;
using Prism.AppModel;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace AP.MobileToolkit.Mvvm
{
    public abstract class ReactiveViewModelBase : ReactiveObject, IActiveAware, IInitialize, IInitializeAsync, INavigatedAware, IDestructible, IConfirmNavigation, IConfirmNavigationAsync, IApplicationLifecycleAware, IPageLifecycleAware
    {
        protected INavigationService _navigationService { get; }
        protected IPageDialogService _pageDialogService { get; }
        protected ILogger _logger { get; }

        public ReactiveViewModelBase(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _logger = logger;

            Title = GetType().SanitizeViewModelTypeName();
            NavigateCommand = ReactiveCommand.CreateFromTask<string>(OnNavigateCommandExecuted, 
                this.WhenAnyObservable(x => x.NavigateCommand.IsExecuting)
                            .Select(isExecuting => !isExecuting)
                            .StartWith(true));
            _isBusyHelper = GetIsBusyProperty();
            _isNotBusyHelper = this.WhenAnyValue(x => x.IsBusy)  
                                   .Select(x => !IsBusy)
                                   .ToProperty(this, x => x.IsNotBusy, true);

            IsActiveChangedCommand = ReactiveCommand.Create(OnIsActiveChanged);
            this.WhenAnyValue(x => x.IsActive)
                .Select(x => Unit.Default)
                .InvokeCommand(IsActiveChangedCommand);
        }

        protected virtual ObservableAsPropertyHelper<bool> GetIsBusyProperty()
        {
            return this.WhenAnyObservable(x => x.NavigateCommand.IsExecuting)
                .ToProperty(this, x => x.IsBusy, false);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        private string _subtitle;
        public string Subtitle
        {
            get => _subtitle;
            set => this.RaiseAndSetIfChanged(ref _subtitle, value);
        }

        protected ObservableAsPropertyHelper<bool> _isBusyHelper;
        public bool IsBusy => _isBusyHelper?.Value ?? false;

        private ObservableAsPropertyHelper<bool> _isNotBusyHelper;
        public bool IsNotBusy => _isNotBusyHelper.Value;

        public ReactiveCommand<string, Unit> NavigateCommand { get; private set; }

        protected virtual async Task OnNavigateCommandExecuted(string uri)
        {
            await HandleNavigationRequest(uri);
        }

        protected virtual Task HandleNavigationRequest(string uri) => HandleNavigationRequest(uri, null);

        protected virtual async Task HandleNavigationRequest(string uri, INavigationParameters parameters)
        {
            try
            {
                var result = await _navigationService.NavigateAsync(uri, parameters);
                if (result.Exception != null)
                {
                    await HandleNavigationException(uri, parameters, result.Exception);
                }
            }
            catch (Exception ex)
            {
                await HandleNavigationException(uri, parameters, ex);
            }
        }

        protected virtual async Task HandleNavigationException(string uri, INavigationParameters parameters, Exception ex)
        {
            var correlationId = Guid.NewGuid().ToString();
            var errorParameters = parameters.ToErrorParameters(uri);
            errorParameters.Add("CorrelationId", correlationId);
            _logger.Report(ex, errorParameters);
            await DisplayAlertForException(ex, correlationId);
        }

        protected virtual async Task DisplayAlertForException(Exception ex, string correlationId)
        {
            await _pageDialogService.DisplayAlertAsync(
                            ToolkitResources.Error, 
                            string.Format(ToolkitResources.AlertErrorMessageTemplate, ex.GetType().Name, correlationId), 
                            ToolkitResources.Ok);
        }

        #region IActiveAware

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set => this.RaiseAndSetIfChanged(ref _isActive, value);
        }

        public event EventHandler IsActiveChanged;

        private ReactiveCommand<Unit, Unit> IsActiveChangedCommand;

        private void OnIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);

            if (IsActive)
            {
                OnIsActive();
            }
            else
            {
                OnIsNotActive();
            }
        }

        protected virtual void OnIsActive() { }

        protected virtual void OnIsNotActive() { }

        #endregion IActiveAware

        #region IInitialize

        protected virtual void Initialize(INavigationParameters parameters) { }

        protected virtual Task InitializeAsync(INavigationParameters parameters) => Task.CompletedTask;

        void IInitialize.Initialize(INavigationParameters parameters) => Initialize(parameters);

        Task IInitializeAsync.InitializeAsync(INavigationParameters parameters) => InitializeAsync(parameters);

        #endregion IInitialize

        #region INavigatedAware

        protected virtual void OnNavigatedTo(INavigationParameters parameters) { }

        protected virtual void OnNavigatedFrom(INavigationParameters parameters) { }

        void INavigatedAware.OnNavigatedTo(INavigationParameters parameters) => OnNavigatedTo(parameters);

        void INavigatedAware.OnNavigatedFrom(INavigationParameters parameters) => OnNavigatedFrom(parameters);

        #endregion INavigatedAware

        #region IDestructible

        protected virtual void Destroy() { }

        void IDestructible.Destroy()
        {
            if(NavigateCommand != null)
            {
                NavigateCommand.Dispose();
                NavigateCommand = null;
            }

            if(IsActiveChangedCommand != null)
            {
                IsActiveChangedCommand.Dispose();
                IsActiveChangedCommand = null;
            }

            if (_isBusyHelper != null)
            {
                _isBusyHelper.Dispose();
                _isBusyHelper = null;
            }

            if (_isNotBusyHelper != null)
            {
                _isNotBusyHelper.Dispose();
                _isNotBusyHelper = null;
            }

            Destroy();
        }

        #endregion IDestructible

        #region IConfirmNavigation

        protected virtual bool CanNavigate(INavigationParameters parameters) => true;

        protected virtual Task<bool> CanNavigateAsync(INavigationParameters parameters) =>
            Task.FromResult(true);

        bool IConfirmNavigation.CanNavigate(INavigationParameters parameters) => CanNavigate(parameters);

        Task<bool> IConfirmNavigationAsync.CanNavigateAsync(INavigationParameters parameters) => CanNavigateAsync(parameters);

        #endregion IConfirmNavigation

        #region IApplicationLifecycleAware

        protected virtual void OnResume() { }

        protected virtual void OnSleep() { }

        void IApplicationLifecycleAware.OnResume() => OnResume();

        void IApplicationLifecycleAware.OnSleep() => OnSleep();

        #endregion IApplicationLifecycleAware

        #region IPageLifecycleAware

        protected virtual void OnAppearing() { }

        protected virtual void OnDisappearing() { }

        void IPageLifecycleAware.OnAppearing() => OnAppearing();

        void IPageLifecycleAware.OnDisappearing() => OnDisappearing();

        #endregion IPageLifecycleAware
    }
}
