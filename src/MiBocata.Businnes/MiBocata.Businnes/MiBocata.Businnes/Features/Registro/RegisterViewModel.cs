using Mibocata.Core.Features.Auth;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Helpers;
using MiBocata.Businnes.Services.Commons.Navigation;
using MiBocata.Businnes.Services.Commons.Preferences;
using Models;
using Models.Core;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MiBocata.Businnes.Features.Registro
{
    public class RegisterViewModel : BaseViewModel
    {
        private Shopkeeper todoItem;
        private readonly IAuthApi authApi;
        private readonly IKeyboardService keyboardService;

        public RegisterViewModel(
          INavigationService navigationService,
          IMiBocataNavigationService miBocataNavigationService,
          IPreferencesService preferencesService,
          ISessionService sessionService,
          ILoggingService loggingService,
          IDialogService dialogService,
          IConnectivityService connectivityService,
          ITaskHelper taskHelper,
          IRefitService refitService,
          ITaskHelperFactory taskHelperFactory, 
          IKeyboardService keyboardService)
          : base(
                navigationService,
                miBocataNavigationService,
                preferencesService,
                sessionService,
                loggingService,
                dialogService,
                connectivityService,
                taskHelper,
                refitService,
                taskHelperFactory)
        {
            this.authApi = RefitService.InitRefitInstance<IAuthApi>();
            this.keyboardService = keyboardService;
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
            await NavigationService.NavigateBackAsync();
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

            var result = await TaskHelperFactory.
                CreateInternetAccessViewModelInstance(LoggingService, this).
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
            PreferencesService.SetUser(result);
            await MiBocataNavigationService.NavigateToChooseLocationStore();
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
