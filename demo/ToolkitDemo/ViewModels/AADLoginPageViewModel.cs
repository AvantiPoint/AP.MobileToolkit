using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using AP.MobileToolkit.AAD;
using Microsoft.Identity.Client;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace ToolkitDemo.ViewModels
{
    public class AADLoginPageViewModel : DemoPageViewModelBase
    {
        private IAuthenticationService AuthenticationService { get; }

        public AADLoginPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger, IAuthenticationService authenticationService)
            : base(navigationService, pageDialogService, logger)
        {
            Title = "Login";
            AuthenticationService = authenticationService;

            LoginCommand = ReactiveCommand.CreateFromTask(
                OnLoginCommandExecuted,
                this.WhenAnyValue(x => x.IsBusy)
                .Select(x => !x));

            LogoutCommand = ReactiveCommand.CreateFromTask(OnLogoutCommandExecuted);
        }

        public MsalUser User
        {
            get => _user;
            set => this.RaiseAndSetIfChanged(ref _user, value);
        }

        private MsalUser _user;

        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set => this.RaiseAndSetIfChanged(ref _isLoggedIn, value);
        }

        private bool _isLoggedIn;

        public ReactiveCommand<Unit, Unit> LoginCommand { get; }

        public ReactiveCommand<Unit, Unit> LogoutCommand { get; }

        protected override ObservableAsPropertyHelper<bool> GetIsBusyProperty()
        {
            return this.WhenAnyObservable(x => x.LoginCommand.IsExecuting)
                .ToProperty(this, x => x.IsBusy, false);
        }

        protected async override void OnAppearing()
        {
            if (await LoginCommand.CanExecute.FirstAsync())
            {
                await LoginCommand.Execute();
            }
        }

        private async Task OnLoginCommandExecuted()
        {
            try
            {
                var result = await AuthenticationService.LoginAsync();

                if (!(result is null))
                {
                    IsLoggedIn = true;
                    User = new MsalUser(result.AccessToken);
                }
            }
            catch (Exception ex)
            {
                var data = new Dictionary<string, string>
                {
                    { "page", "Login" }
                };

                if (ex is MsalException msal)
                {
                    data.Add("errorCode", msal.ErrorCode);
                    if (msal.ErrorCode == MsalError.AuthenticationCanceledError)
                    {
                        Logger.TrackEvent("User Canceled Login");
                        return;
                    }
                }

                Logger.Report(ex, data);
            }
        }

        private async Task OnLogoutCommandExecuted()
        {
            try
            {
                await AuthenticationService.LogoutAsync();
                User = null;
                IsLoggedIn = false;
            }
            catch (Exception ex)
            {
                var data = new Dictionary<string, string>
                {
                    { "page", "Login" }
                };

                if (ex is MsalException msal)
                {
                    data.Add("errorCode", msal.ErrorCode);
                }

                Logger.Report(ex, data);
            }
        }
    }
}
