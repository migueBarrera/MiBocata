using System.Threading.Tasks;
using System.Windows.Input;
using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Helpers;
using MiBocata.Businnes.Services.Commons.Navigation;
using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using Models.Core;
using Xamarin.Forms;

namespace MiBocata.Businnes.Features.LogIn
{
    public class LogInViewModel : CoreViewModel
    {
        private Shopkeeper todoItem;
        private readonly ILogInService logInService;
        private readonly IMiBocataNavigationService miBocataNavigationService;
        private readonly IDialogService dialogService;
        private readonly IKeyboardService keyboardService;

        public LogInViewModel(
            ILogInService logInService,
            IMiBocataNavigationService miBocataNavigationService,
            IDialogService dialogService,
            IKeyboardService keyboardService)
        {
            this.logInService = logInService;
            this.miBocataNavigationService = miBocataNavigationService;
            this.dialogService = dialogService;
            this.keyboardService = keyboardService;
        }

        public Shopkeeper User
        {
            get => todoItem;
            set => SetAndRaisePropertyChanged(ref todoItem, value);
        }

        public ICommand CallUsCommand => new AsyncCommand(_ => CallUsCommandd());

        public ICommand DoLoginCommand => new AsyncCommand(_ => DoLoginCommandAsync());

        public ICommand GoToRegisterCommand => new AsyncCommand(_ => GoToRegisterCommandAsync());

        public override Task InitializeAsync(object navigationData)
        {
            User = new Shopkeeper();

#pragma warning disable CS0162 // Unreachable code detected
            if (DefaultSettings.DebugMode)
            {
                User = new Shopkeeper()
                {
                    Email = "mbmdevelop@gmail.com",
                    Password = "123456",
                };
            }
#pragma warning restore CS0162 // Unreachable code detected

            return base.InitializeAsync(navigationData);
        }

        private async Task CallUsCommandd()
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                await dialogService.ShowAlertAsync("603033613", "Llamenos sin compromiso");
                return;
            }

            Xamarin.Essentials.PhoneDialer.Open("603033613");
        }

        private async Task DoLoginCommandAsync()
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

            await logInService.DoLoginAsync(User.Email, User.Password);
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

        private async Task GoToRegisterCommandAsync()
        {
            await miBocataNavigationService.NavigateToRegister();
        }
    }
}
