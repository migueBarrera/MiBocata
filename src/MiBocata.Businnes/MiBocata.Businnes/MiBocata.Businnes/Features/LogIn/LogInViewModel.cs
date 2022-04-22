using System.Threading.Tasks;
using System.Windows.Input;
using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Helpers;
using MiBocata.Businnes.Services.Commons.Navigation;
using MiBocata.Businnes.Services.Commons.Preferences;
using Mibocata.Core.Features.Auth;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Features.Stores;
using Mibocata.Core.Services.Interfaces;
using Models;
using Models.Core;
using Xamarin.Forms;

namespace MiBocata.Businnes.Features.LogIn
{
    public class LogInViewModel : BaseViewModel
    {
        private Shopkeeper todoItem;
        private readonly IAuthApi authApi;
        private readonly IStoreApi storeApi;
        private readonly IKeyboardService keyboardService;

        public LogInViewModel(
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
            this.storeApi = RefitService.InitRefitInstance<IStoreApi>(isAutenticated: true);
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
                await DialogService.ShowAlertAsync("603033613", "Llamenos sin compromiso");
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

            var result = await TaskHelperFactory.
                CreateInternetAccessViewModelInstance(LoggingService, this).
                TryExecuteAsync(
                () => authApi.SignIn(new Models.Requests.ShopkeeperSignInRequest()
                {
                    Email = User.Email,
                    Password = User.Password,
                }));

            if (result)
            {
                //TODO
                await LogInSuccessful(new Shopkeeper()
                {
                    Email = result.Value.Email,
                    Name = result.Value.Name,
                    Id = result.Value.Id,
                });
            }
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

        private async Task LogInSuccessful(Shopkeeper shopkeeper)
        {
            PreferencesService.SetUser(shopkeeper);

            var pushToken = PreferencesService.PushToken();

            if (shopkeeper.IdStore != 0)
            {
                var store = await storeApi.Get(shopkeeper.IdStore);
                PreferencesService.SetStore(new Store()
                {
                    Id = store.Id,
                    Name = store.Name,
                    Image = store.Image,
                    AutoAccept = store.AutoAccept,
                    Products = store.Products,
                });
                ////store.PushToken = pushToken;
                ////await StoreManager.DefaultManager.UpdateItemAsync(store);
                await MiBocataNavigationService.NavigateToHome();
            }
            else
            {
                await MiBocataNavigationService.NavigateToChooseLocationStore();
            }
        }

        private async Task GoToRegisterCommandAsync()
        {
            await MiBocataNavigationService.NavigateToRegister();
        }
    }
}
