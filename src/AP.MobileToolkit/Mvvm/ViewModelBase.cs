using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AP.CrossPlatform;
using AP.MobileToolkit.Extensions;
using AP.MobileToolkit.Resources;
using Prism;
using Prism.AppModel;
using Prism.Commands;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;

namespace AP.MobileToolkit.Mvvm
{
    public abstract class ViewModelBase : ObservableObject, IActiveAware, INavigationAware, IDestructible, IConfirmNavigation, IConfirmNavigationAsync, IApplicationLifecycleAware, IPageLifecycleAware
    {
        protected INavigationService NavigationService { get; }
        protected IPageDialogService PageDialogService { get; }
        protected ILogger Logger { get; }

        public ViewModelBase(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
        {
            NavigationService = navigationService;
            PageDialogService = pageDialogService;
            Logger = logger;
            Title = Regex.Replace(GetType().Name, "ViewModel$", "");
            NavigateCommand = new DelegateCommand<string>(OnNavigateCommandExecuted, s => IsNotBusy).ObservesProperty(() => IsNotBusy);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _subtitle;
        public string Subtitle
        {
            get => _subtitle;
            set => SetProperty(ref _subtitle, value);
        }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value, onChanged: OnIsBusyChanged);
        }

        private bool _isNotBusy = true;
        public bool IsNotBusy
        {
            get => _isNotBusy;
            set => SetProperty(ref _isNotBusy, value, onChanged: OnIsNotBusyChanged);
        }

        private bool _canLoadMore;
        public bool CanLoadMore
        {
            get => _canLoadMore;
            set => SetProperty(ref _canLoadMore, value);
        }

        private string _header;
        public string Header
        {
            get => _header;
            set => SetProperty(ref _header, value);
        }

        private string _footer;
        public string Footer
        {
            get => _footer;
            set => SetProperty(ref _footer, value);
        }

        private void OnIsBusyChanged() => IsNotBusy = !IsBusy;

        private void OnIsNotBusyChanged() => IsBusy = !IsNotBusy;

        public DelegateCommand<string> NavigateCommand { get; }

        protected virtual async void OnNavigateCommandExecuted(string uri)
        {
            await HandleNavigationRequest(uri);
        }

        protected virtual Task HandleNavigationRequest(string uri) => HandleNavigationRequest(uri, null);

        protected virtual async Task HandleNavigationRequest(string uri, INavigationParameters parameters)
        {
            IsBusy = true;

            try
            {
                var result = await NavigationService.NavigateAsync(uri, parameters);
                if(result.Exception != null)
                {
                    Logger.Info("Navigation Error", parameters.ToErrorParameters(uri, result.Exception));
                    Logger.Report(result.Exception, parameters.ToErrorParameters(uri));
                    await DisplayAlertForException(result.Exception);
                }
            }
            catch (Exception ex)
            {
                Logger.Info("Navigation Error", parameters.ToErrorParameters(uri, ex));
                Logger.Report(ex, parameters.ToErrorParameters(uri));
                await DisplayAlertForException(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected virtual async Task DisplayAlertForException(Exception ex)
        {
            await PageDialogService.DisplayAlertAsync(ex.GetType().Name, ex.Message, ToolkitResources.Ok);
        }

        #region IActiveAware

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value, OnIsActiveChanged);
        }

        public event EventHandler IsActiveChanged;

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

        #region INavigationAware

        public virtual void OnNavigatingTo(INavigationParameters parameters) { }

        public virtual void OnNavigatedTo(INavigationParameters parameters) { }

        public virtual void OnNavigatedFrom(INavigationParameters parameters) { }

        #endregion INavigationAware

        #region IDestructible

        protected virtual void Destroy() { }

        void IDestructible.Destroy() => Destroy();

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
