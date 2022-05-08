using System.Windows.Input;
using MiBocata.Businnes.Helpers;
using MiBocata.Businnes.Services.Commons.Navigation;
using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using Models.Core;

namespace MiBocata.Businnes.Features.Registro
{
    public class RegisterViewModel : CoreViewModel
    {
        private Shopkeeper todoItem;
        private readonly IRegisterService registerService;
        private readonly IDialogService dialogService;
        private readonly IKeyboardService keyboardService;
        private readonly INavigationService navigationService;

        public RegisterViewModel(
            IRegisterService registerService,
            IDialogService dialogService,
            IKeyboardService keyboardService,
            INavigationService navigationService)
        {
            this.registerService = registerService;
            this.dialogService = dialogService;
            this.keyboardService = keyboardService;
            this.navigationService = navigationService;
        }

        public Shopkeeper User
        {
            get => todoItem;
            set => SetAndRaisePropertyChanged(ref todoItem, value);
        }

        public ICommand RegisterCommand => new AsyncCommand(() => RegisterCommandAsync());

        public ICommand BackCommand => new AsyncCommand(() => BackCommandAsync());

        public ICommand CallUsCommand => new AsyncCommand(() => CallUsCommandd());

        public override async Task OnAppearingAsync()
        {
            User = new Shopkeeper();

            await base.OnAppearingAsync();
        }

        private async Task CallUsCommandd()
        {
            //Xamarin.Essentials.PhoneDialer.Open("603033613");
        }

        private async Task BackCommandAsync()
        {
            await navigationService.NavigateBackAsync();
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

            await registerService.RegisterCommandAsync(User);
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
