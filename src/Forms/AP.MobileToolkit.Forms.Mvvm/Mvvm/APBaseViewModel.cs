using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using AP.CrossPlatform.Extensions;
using AP.CrossPlatform.i18n;
using AP.MobileToolkit.Extensions;
using AP.MobileToolkit.Resources;
using Prism.AppModel;
using Prism.Events;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using ReactiveUI;

namespace AP.MobileToolkit.Mvvm
{
    /// <summary>
    /// Provides a base ViewModel Class
    /// </summary>
    public abstract class APBaseViewModel : ReactiveObject, IInitialize, IInitializeAsync, INavigatedAware, IDestructible, IConfirmNavigation, IConfirmNavigationAsync, IApplicationLifecycleAware, IPageLifecycleAware
    {
        protected CompositeDisposable Disposables { get; private set; }

        protected INavigationService NavigationService { get; private set; }

        protected IDeviceService DeviceService { get; }

        protected IDialogService DialogService { get; }

        protected ILogger Logger { get; }

        protected IEventAggregator EventAggregator { get; }

        protected IPageDialogService PageDialogService { get; }

        protected ILocalize Localize { get; }

        protected APBaseViewModel(BaseServices baseServices)
        {
            Disposables = new CompositeDisposable();
            NavigationService = baseServices.NavigationService;
            DeviceService = baseServices.DeviceService;
            DialogService = baseServices.DialogService;
            Localize = baseServices.Localize;
            Logger = baseServices.Logger;
            EventAggregator = baseServices.EventAggregator;
            PageDialogService = baseServices.PageDialogService;

            Title = GetTitle();
            _isNotBusyHelper = this.WhenAnyValue(x => x.IsBusy)
                .Select(x => !x)
                .ToProperty(this, nameof(IsNotBusy), true, true)
                .DisposeWith(Disposables);

            NavigateCommand = ReactiveCommand.CreateFromTask<string>(
                OnNavigateCommandExecuted, this.WhenAnyValue(x => x.IsNotBusy))
                .DisposeWith(Disposables);
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

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "This is a backing field though it needs to be accessible to inheriting classes.")]
        protected ObservableAsPropertyHelper<bool> _isBusyHelper;

        public bool IsBusy => _isBusyHelper?.Value ?? false;

        private ObservableAsPropertyHelper<bool> _isNotBusyHelper;

        public bool IsNotBusy => _isNotBusyHelper.Value;

        public ReactiveCommand<string, Unit> NavigateCommand { get; private set; }

        // Not used for binding so this is not a reactive property
        protected string CallbackUrl { get; set; }

        protected virtual async Task OnNavigateCommandExecuted(string uri)
        {
            await HandleNavigationRequest(uri);
        }

        protected virtual Task HandleNavigationRequest(string uri) => HandleNavigationRequest(uri, null);

        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "This is meant to catch any unexpected exceptions")]
        protected virtual async Task HandleNavigationRequest(string uri, INavigationParameters parameters)
        {
            try
            {
                var result = await NavigationService.NavigateAsync(uri, parameters);
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
            Logger.Report(ex, errorParameters);
            await DisplayAlertForException(ex, correlationId);
        }

        protected virtual async Task DisplayAlertForException(Exception ex, string correlationId)
        {
            await PageDialogService.DisplayAlertAsync(
                            ToolkitResources.Error,
                            string.Format(ToolkitResources.AlertErrorMessageTemplate, ex.ToErrorMessage(), correlationId),
                            ToolkitResources.Ok);
        }

        protected virtual string GetTitle()
        {
            var viewmodelType = GetType();
            var title = Localize[viewmodelType.Name];
            if (!string.IsNullOrEmpty(title))
            {
                return title;
            }

            return viewmodelType.SanitizeViewModelTypeName();
        }

        #region IInitialize

        protected virtual void Initialize(INavigationParameters parameters)
        {
        }

        protected virtual Task InitializeAsync(INavigationParameters parameters) => Task.CompletedTask;

        void IInitialize.Initialize(INavigationParameters parameters)
        {
            if (parameters.TryGetValue<string>("callback", out var callback))
            {
                CallbackUrl = callback;
            }

            Initialize(parameters);
        }

        Task IInitializeAsync.InitializeAsync(INavigationParameters parameters) => InitializeAsync(parameters);

        #endregion IInitialize

        #region INavigatedAware

        protected virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        protected virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        void INavigatedAware.OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue<string>("callback", out var callback))
            {
                CallbackUrl = callback;
            }

            OnNavigatedTo(parameters);
        }

        void INavigatedAware.OnNavigatedFrom(INavigationParameters parameters) => OnNavigatedFrom(parameters);

        #endregion INavigatedAware

        #region IDestructible

        protected virtual void Destroy()
        {
        }

        void IDestructible.Destroy()
        {
            Destroy();
            NavigationService = null;
            Disposables.Dispose();
            Disposables = null;
            _isNotBusyHelper = null;
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

        protected virtual void OnResume()
        {
        }

        protected virtual void OnSleep()
        {
        }

        void IApplicationLifecycleAware.OnResume() => OnResume();

        void IApplicationLifecycleAware.OnSleep() => OnSleep();

        #endregion IApplicationLifecycleAware

        #region IPageLifecycleAware

        protected virtual void OnAppearing()
        {
        }

        protected virtual void OnDisappearing()
        {
        }

        void IPageLifecycleAware.OnAppearing() => OnAppearing();

        void IPageLifecycleAware.OnDisappearing() => OnDisappearing();

        #endregion IPageLifecycleAware
    }
}
