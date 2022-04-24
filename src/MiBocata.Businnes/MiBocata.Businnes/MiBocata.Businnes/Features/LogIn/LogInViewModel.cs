using System.Threading.Tasks;
using System.Windows.Input;
using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Helpers;
using MiBocata.Businnes.Services.Commons.Navigation;
using MiBocata.Businnes.Services.Commons.Preferences;
using Mibocata.Core.Features.Auth;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Features.Stores;
using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using Models.Core;
using Xamarin.Forms;

namespace MiBocata.Businnes.Features.LogIn
{
    public class LogInViewModel : CoreViewModel
    {
        private Shopkeeper todoItem;
        private readonly IAuthApi authApi;
        private readonly IStoreApi storeApi;
        private readonly IMiBocataNavigationService miBocataNavigationService;
        private readonly IPreferencesService preferencesService;
        private readonly ILoggingService loggingService;
        private readonly IDialogService dialogService;
        private readonly ITaskHelperFactory taskHelperFactory;
        private readonly IKeyboardService keyboardService;

        public LogInViewModel(
            IMiBocataNavigationService miBocataNavigationService,
            IPreferencesService preferencesService,
            ILoggingService loggingService,
            IDialogService dialogService,
            IRefitService refitService,
            ITaskHelperFactory taskHelperFactory, 
            IKeyboardService keyboardService)
        {
            this.authApi = refitService.InitRefitInstance<IAuthApi>();
            this.storeApi = refitService.InitRefitInstance<IStoreApi>(isAutenticated: true);
            this.miBocataNavigationService = miBocataNavigationService;
            this.preferencesService = preferencesService;
            this.loggingService = loggingService;
            this.dialogService = dialogService;
            this.taskHelperFactory = taskHelperFactory;
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

            var result = await taskHelperFactory.
                CreateInternetAccessViewModelInstance(loggingService, this).
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

        private async Task LogInSuccessful(Shopkeeper shopkeeper)
        {
            preferencesService.SetUser(shopkeeper);

            var pushToken = preferencesService.PushToken();

            if (shopkeeper.IdStore != 0)
            {
                var store = await storeApi.Get(shopkeeper.IdStore);
                preferencesService.SetStore(new Store()
                {
                    Id = store.Id,
                    Name = store.Name,
                    Image = store.Image,
                    AutoAccept = store.AutoAccept,
                    Products = store.Products,
                });
                ////store.PushToken = pushToken;
                ////await StoreManager.DefaultManager.UpdateItemAsync(store);
                await miBocataNavigationService.NavigateToHome();
            }
            else
            {
                await miBocataNavigationService.NavigateToChooseLocationStore();
            }
        }

        private async Task GoToRegisterCommandAsync()
        {
            await miBocataNavigationService.NavigateToRegister();
        }
    }
}
