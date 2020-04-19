using MiBocata.Framework;
using MiBocata.Helpers;
using MiBocata.Services.API.Interfaces;
using Models;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiBocata.Features.Register
{
    public class RegisterViewModel : BaseViewModel
    {
        private Client user;
        private readonly IAuthApi authApi;

        public RegisterViewModel()
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
                                () => authApi.SignUp(User));

            if (result)
            {
                await RegisterSuccessesful(result.Value);
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
