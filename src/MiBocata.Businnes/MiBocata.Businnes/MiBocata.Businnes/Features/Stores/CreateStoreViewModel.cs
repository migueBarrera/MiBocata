using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Features.Stores;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Services.Commons.Navigation;
using MiBocata.Businnes.Services.Commons.Preferences;
using Models.Core;
using Xamarin.Forms.Maps;
using static MiBocata.Businnes.Features.Stores.ChooseLocationViewModel;

namespace MiBocata.Businnes.Features.Stores
{
    public class CreateStoreViewModel : BaseViewModel
    {
        private Store store;
        private readonly IStoreApi storeApi;

        private IEnumerable<Model> locations;

        public CreateStoreViewModel(
           INavigationService navigationService,
           IMiBocataNavigationService miBocataNavigationService,
           IPreferencesService preferencesService,
           ISessionService sessionService,
           ILoggingService loggingService,
           IDialogService dialogService,
           IConnectivityService connectivityService,
           ITaskHelper taskHelper,
           IRefitService refitService,
           ITaskHelperFactory taskHelperFactory)
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
            storeApi = RefitService.InitRefitInstance<IStoreApi>(isAutenticated: true);
        }

        public Store Store { get => store; set => SetAndRaisePropertyChanged(ref store, value); }

        public IEnumerable<Model> Locations { get => locations; set => SetAndRaisePropertyChanged(ref locations, value); }

        public ICommand NextCommand => new AsyncCommand(_ => NextCommandAsync());

        public override Task InitializeAsync(object navigationData)
        {
            IsBusy = false;
            Store = SessionService.Get<Store>(nameof(Store));
            Locations = new List<Model>()
            {
                new Model()
                {
                    Position = new Position(Store.StoreLocation.Latitude, Store.StoreLocation.Longitude),
                    Description = "Tu tienda",
                    Address = string.Empty,
                },
            };
            return base.InitializeAsync(navigationData);
        }

        private async Task NextCommandAsync()
        {
            if (!Validate())
            {
                return;
            }

            ////var pushToken = PreferencesService.PushToken();
            ////todo store.PushToken = pushToken;

            var result = await TaskHelperFactory.
                                    CreateInternetAccessViewModelInstance(LoggingService, this).
                                    TryExecuteAsync(() => storeApi.Create(new Models.Requests.StoreCreateRequest()
                                    {
                                        //store
                                    }));

            if (result)
            {
                await OnCreateStoreSuccessful(new Store()
                {
                    Id = result.Value.Id,
                    Name = result.Value.Name,
                    Products = result.Value.Products,
                    AutoAccept = result.Value.AutoAccept,
                    Image = result.Value.Image,
                    StoreLocation = result.Value.StoreLocation,
                });
            }
        }

        private async Task OnCreateStoreSuccessful(Store storeResult)
        {
            SessionService.Clear();
            PreferencesService.SetStore(storeResult);

            await MiBocataNavigationService.NavigateToHome();
        }

        private bool Validate()
        {
            return true;
        }
    }
}
