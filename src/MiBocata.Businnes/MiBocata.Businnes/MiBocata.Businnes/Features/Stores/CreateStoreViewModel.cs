using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Services.Commons.Navigation;
using MiBocata.Businnes.Services.Commons.Preferences;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Features.Stores;
using Mibocata.Core.Services.Interfaces;
using Models.Core;
using Xamarin.Forms.Maps;
using static MiBocata.Businnes.Features.Stores.ChooseLocationViewModel;
using Mibocata.Core.Framework;

namespace MiBocata.Businnes.Features.Stores
{
    public class CreateStoreViewModel : CoreViewModel
    {
        private Store store;
        private readonly IStoreApi storeApi;
        private readonly IMiBocataNavigationService miBocataNavigationService;
        private readonly IPreferencesService preferencesService;
        private readonly ISessionService sessionService;
        private readonly ILoggingService loggingService;
        private readonly ITaskHelperFactory taskHelperFactory;
        private IEnumerable<Model> locations;

        public CreateStoreViewModel(
           IMiBocataNavigationService miBocataNavigationService,
           IPreferencesService preferencesService,
           ISessionService sessionService,
           ILoggingService loggingService,
           IRefitService refitService,
           ITaskHelperFactory taskHelperFactory)
        {
            storeApi = refitService.InitRefitInstance<IStoreApi>(isAutenticated: true);
            this.miBocataNavigationService = miBocataNavigationService;
            this.preferencesService = preferencesService;
            this.sessionService = sessionService;
            this.loggingService = loggingService;
            this.taskHelperFactory = taskHelperFactory;
        }

        public Store Store { get => store; set => SetAndRaisePropertyChanged(ref store, value); }

        public IEnumerable<Model> Locations { get => locations; set => SetAndRaisePropertyChanged(ref locations, value); }

        public ICommand NextCommand => new AsyncCommand(_ => NextCommandAsync());

        public override Task InitializeAsync(object navigationData)
        {
            IsBusy = false;
            Store = sessionService.Get<Store>(nameof(Store));
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

            var result = await taskHelperFactory.
                                    CreateInternetAccessViewModelInstance(loggingService, this).
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
            sessionService.Clear();
            preferencesService.SetStore(storeResult);

            await miBocataNavigationService.NavigateToHome();
        }

        private bool Validate()
        {
            return true;
        }
    }
}
