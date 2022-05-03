using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Helpers;
using Models.Core;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace MiBocata.Features.Register
{
    public class RegisterViewModel : CoreViewModel
    {
        private Client user;

        private readonly IDialogService dialogService;
        private readonly IRegisterService registerService;
        private readonly IKeyboardService keyboardService;

        public RegisterViewModel(
            IDialogService dialogService,
            IRegisterService registerService,
            IKeyboardService keyboardService)
        {
            this.dialogService = dialogService;
            this.registerService = registerService;
            this.keyboardService = keyboardService;
        }

        public Client User
        {
            get => user;
            set => SetAndRaisePropertyChanged(ref user, value);
        }

        public ICommand RegisterCommand => new AsyncCommand(() => RegisterCommandAsync());

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

            await registerService.DoRegisterAsync(user.Email, user.Password, user.Name);
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
