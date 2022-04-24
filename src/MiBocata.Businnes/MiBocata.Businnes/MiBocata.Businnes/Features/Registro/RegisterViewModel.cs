using System.Threading.Tasks;
using System.Windows.Input;
using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Helpers;
using MiBocata.Businnes.Services.Commons.Navigation;
using MiBocata.Businnes.Services.Commons.Preferences;
using Mibocata.Core.Features.Auth;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Services.Interfaces;
using Models;
using Models.Core;
using Xamarin.Forms;
using Mibocata.Core.Framework;

namespace MiBocata.Businnes.Features.Registro
{
    public class RegisterViewModel : CoreViewModel
    {
        private Shopkeeper todoItem;
        private readonly IAuthApi authApi;
        private readonly IMiBocataNavigationService miBocataNavigationService;
        private readonly IPreferencesService preferencesService;
        private readonly ILoggingService loggingService;
        private readonly IDialogService dialogService;
        private readonly ITaskHelperFactory taskHelperFactory;
        private readonly IKeyboardService keyboardService;
        private readonly INavigationService navigationService;

        public RegisterViewModel(
          IMiBocataNavigationService miBocataNavigationService,
          IPreferencesService preferencesService,
          ILoggingService loggingService,
          IDialogService dialogService,
          IRefitService refitService,
          ITaskHelperFactory taskHelperFactory,
          IKeyboardService keyboardService, 
          INavigationService navigationService)
        {
            this.authApi = refitService.InitRefitInstance<IAuthApi>();
            this.miBocataNavigationService = miBocataNavigationService;
            this.preferencesService = preferencesService;
            this.loggingService = loggingService;
            this.dialogService = dialogService;
            this.taskHelperFactory = taskHelperFactory;
            this.keyboardService = keyboardService;
            this.navigationService = navigationService;
        }

        public Shopkeeper User
        {
            get => todoItem;
            set => SetAndRaisePropertyChanged(ref todoItem, value);
        }

        public ICommand RegisterCommand => new AsyncCommand(_ => RegisterCommandAsync());

        public ICommand BackCommand => new AsyncCommand(_ => BackCommandAsync());

        public ICommand CallUsCommand => new Command(_ => CallUsCommandd());

        public override async Task InitializeAsync(object navigationData)
        {
            User = new Shopkeeper();

            await base.InitializeAsync(navigationData);
        }

        private void CallUsCommandd()
        {
            Xamarin.Essentials.PhoneDialer.Open("603033613");
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

            var result = await taskHelperFactory.
                CreateInternetAccessViewModelInstance(loggingService, this).
                TryExecuteAsync(
                () => authApi.SignUp(new Models.Requests.ShopkeeperSignUpRequest()
                {
                    Email = User.Email,
                    Name = User.Name,
                    Password = User.Password,
                }));

            //TODO
            if (result)
            {
                await RegisterSuccessesful(new Shopkeeper()
                {
                    Id = result.Value.Id,
                    Email = result.Value.Email,
                    Name = result.Value.Name,
                });
            }
        }

        private async Task RegisterSuccessesful(Shopkeeper result)
        {
            preferencesService.SetUser(result);
            await miBocataNavigationService.NavigateToChooseLocationStore();
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
