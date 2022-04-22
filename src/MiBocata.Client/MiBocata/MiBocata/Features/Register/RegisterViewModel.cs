using Mibocata.Core.Features.Auth;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Framework;
using MiBocata.Helpers;
using MiBocata.Services.NavigationService;
using MiBocata.Services.PreferencesService;
using Models.Core;
using Models.Responses;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiBocata.Features.Register
{
    public class RegisterViewModel : BaseViewModel
    {
        private Client user;
        private readonly IAuthApi authApi;

        public RegisterViewModel(
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
            get => user;
            set => SetAndRaisePropertyChanged(ref user, value);
        }

        public ICommand RegisterCommand => new AsyncCommand(_ => RegisterCommandAsync());

        public override async Task InitializeAsync(object navigationData)
        {
            User = new Client();

            await base.InitializeAsync(navigationData);
        }

        private async Task RegisterCommandAsync()
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
                                () => authApi.SignUp(new Models.Requests.ClientSignUpRequest()
                                {
                                    Email = user.Email,
                                    Name = user.Name,
                                    Password = user.Password,
                                }));

            if (result)
            {
                await RegisterSuccessesful(ClientSignUpResponse.Parse(result.Value));
            }
        }

        private async Task RegisterSuccessesful(Client result)
        {
            PreferencesService.SetUser(result);
            await NavigationService.NavigateToHome();
        }

        private async Task<bool> ValidateAsync()
        {
            if (!ValidateHelper.IsValidEmail(User.Email))
            {
                await DialogService.ShowMessage("Compruebe su email", string.Empty);
                return false;
            }

            if (!ValidateHelper.IsValidPassword(User.Password))
            {
                await DialogService.ShowMessage("La contraseña debe tener al menos 4 caracteres", string.Empty);
                return false;
            }

            return true;
        }
    }
}
