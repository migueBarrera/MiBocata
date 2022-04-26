using Mibocata.Core.Features.Auth;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Framework;
using MiBocata.Helpers;
using MiBocata.Services.NavigationService;
using Models.Core;
using Models.Responses;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiBocata.Features.Register
{
    public class RegisterViewModel : CoreViewModel
    {
        private Client user;
        private readonly IAuthApi authApi;
        private readonly IMiBocataNavigationService navigationService;
        private readonly IPreferencesService preferencesService;
        private readonly ILoggingService loggingService;
        private readonly IDialogService dialogService;
        private readonly ITaskHelperFactory taskHelperFactory;
        private readonly IKeyboardService keyboardService;

        public RegisterViewModel(
            IMiBocataNavigationService navigationService,
            IPreferencesService preferencesService,
            ILoggingService loggingService,
            IDialogService dialogService,
            IRefitService refitService,
            ITaskHelperFactory taskHelperFactory,
            IKeyboardService keyboardService)
        {
            this.authApi = refitService.InitRefitInstance<IAuthApi>();
            this.navigationService = navigationService;
            this.preferencesService = preferencesService;
            this.loggingService = loggingService;
            this.dialogService = dialogService;
            this.taskHelperFactory = taskHelperFactory;
            this.keyboardService = keyboardService;
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

            keyboardService.HideSoftKeyboard();

            if (!await ValidateAsync())
            {
                return;
            }

            var result = await taskHelperFactory.
                                CreateInternetAccessViewModelInstance(loggingService, this).
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
            preferencesService.SetClient(result);
            await navigationService.NavigateToHome();
        }

        private async Task<bool> ValidateAsync()
        {
            if (!ValidateHelper.IsValidEmail(User.Email))
            {
                await dialogService.ShowMessage("Compruebe su email", string.Empty);
                return false;
            }

            if (!ValidateHelper.IsValidPassword(User.Password))
            {
                await dialogService.ShowMessage("La contraseña debe tener al menos 4 caracteres", string.Empty);
                return false;
            }

            return true;
        }
    }
}
