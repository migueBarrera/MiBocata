using System.Threading.Tasks;
using System.Windows.Input;
using Mibocata.Core.Features.Auth;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Framework;
using MiBocata.Helpers;
using MiBocata.Services.NavigationService;
using MiBocata.Services.PreferencesService;
using Models.Core;
using Models.Responses;

namespace MiBocata.Features.LogIn
{
    public class LogInControlViewModel : BaseViewModel
    {
        private readonly IAuthApi authApi;
        private Client client;

        public LogInControlViewModel(
            IMiBocataNavigationService navigationService,
            IPreferencesService preferencesService,
            ISessionService sessionService,
            ILoggingService loggingService,
            IDialogService dialogService,
            IConnectivityService connectivityService,
            IRefitService refitService,
            ITaskHelperFactory taskHelperFactory,
            IKeyboardService keyboardService)
            : base(
                  navigationService,
                  preferencesService,
                  sessionService,
                  loggingService,
                  dialogService,
                  connectivityService,
                  refitService,
                  taskHelperFactory,
                  keyboardService)
        {
            this.authApi = RefitService.InitRefitInstance<IAuthApi>();
        }

        public Client User
        {
            get => client;
            set => SetAndRaisePropertyChanged(ref client, value);
        }

        public ICommand DoLoginCommand => new AsyncCommand(_ => DoLoginCommandAsync());

        public ICommand GoToRegisterCommand => new AsyncCommand(_ => GoToRegisterCommandAsync());

        public override Task InitializeAsync(object navigationData = null)
        {
            User = new Client();

#pragma warning disable CS0162 // Unreachable code detected
            if (DefaultSettings.DebugMode)
            {
                User = new Client()
                {
                    Email = "mbmdevelop@gmail.com",
                    Password = "123456",
                };
            }
#pragma warning restore CS0162 // Unreachable code detected

            return base.InitializeAsync(navigationData);
        }

        private async Task DoLoginCommandAsync()
        {
            if (IsBusy)
            {
                return;
            }

            KeyboardService.HideSoftKeyboard();

            if (!await ValidateAsync())
            {
                return;
            }

            var result = await TaskHelperFactory.
                                    CreateInternetAccessViewModelInstance(LoggingService, this).
                                    TryExecuteAsync(
                                    () => authApi.SignIn(new Models.Requests.ClientSignInRequest()
                                    {
                                        Email = User.Email, 
                                        Password = User.Password,
                                    }));

            if (result)
            {
                await LogInSuccessful(ClientSignInResponse.Parse(result.Value));
            }
        }

        private async Task<bool> ValidateAsync()
        {
            if (!ValidateHelper.IsValidEmail(User.Email))
            {
                await DialogService.ShowAlertAsync("Compruebe su email", string.Empty);
                return false;
            }

            if (!ValidateHelper.IsValidPassword(User.Password))
            {
                await DialogService.ShowAlertAsync("La contraseña debe tener al menos 4 caracteres", string.Empty);
                return false;
            }

            return true;
        }

        private async Task LogInSuccessful(Client client)
        {
            PreferencesService.SetUser(client);
            await NavigationService.NavigateToHome();
        }

        private async Task GoToRegisterCommandAsync()
        {
            await NavigationService.NavigateToRegister();
        }
    }
}
