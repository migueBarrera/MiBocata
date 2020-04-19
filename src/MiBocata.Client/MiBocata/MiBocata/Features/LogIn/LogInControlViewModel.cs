using System.Threading.Tasks;
using System.Windows.Input;
using MiBocata.Framework;
using MiBocata.Helpers;
using MiBocata.Services.API.Interfaces;
using MiBocata.Services.TasksServices;
using Models;

namespace MiBocata.Features.LogIn
{
    public class LogInControlViewModel : BaseViewModel
    {
        private readonly IAuthApi authApi;
        private Client client;

        public LogInControlViewModel()
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
                                    () => authApi.SignIn(User));

            if (result)
            {
                await LogInSuccessful(result.Value);
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
