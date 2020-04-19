using MiBocata.Framework;
using MiBocata.Services.API.Interfaces;
using MiBocata.Services.GeolocationService;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms.GoogleMaps;

namespace MiBocata.Features.Stores
{
    public class StoresViewModel : BaseViewModel
    {
        private readonly IStoreApi storeApi;
        private readonly IGeolocationService geolocationService;

        private Location userLocation;

        private IEnumerable<Model> stores;

        public StoresViewModel(
            IGeolocationService geolocationService)
        {
            this.geolocationService = geolocationService;
            this.storeApi = RefitService.InitRefitInstance<IStoreApi>(isAutenticated: false);
        }

        public Location UserLocation { get => userLocation; set => SetAndRaisePropertyChanged(ref userLocation, value); }

        public IEnumerable<Model> Stores { get => stores; set => SetAndRaisePropertyChanged(ref stores, value); }

        public override async Task InitializeAsync(object navigationData)
        {
            await GetStores();

            MainThread.BeginInvokeOnMainThread(async () => 
            {
                UserLocation = await geolocationService.GetLastKnownLocationAsync();
            });

            await base.InitializeAsync(navigationData);
        }

        private async Task GetStores()
        {
            var result = await TaskHelperFactory.
                                    CreateInternetAccessViewModelInstance(LoggingService, this).
                                    TryExecuteAsync(
                                    () => storeApi.GetAll());

            if (result)
            {
                Stores = result.Value.Select(x =>
                               new Model()
                               {
                                   Position = new Position(x.StoreLocation.Latitude, x.StoreLocation.Longitude),
                                   Description = x.Name,
                                   Location = x.StoreLocation,
                                   ////PushToken = x.PushToken,
                                   IdStore = x.Id,
                                   Image = x.Image,
                                   Products = x.Products,
                               })?.ToList();
            }
        }

        public async Task GoToStore(Model model)
        {
            var store = Stores.Where(x => x.IdStore == model.IdStore).FirstOrDefault();
            SessionService.Save(
                "KEY_SESSION_STORE",
                new Store()
                {
                    Name = store.Description,
                    StoreLocation = store.Location,
                    Id = store.IdStore,
                    Products = store.Products,
                    Image = store.Image,
                    ////PushToken = store.PushToken,
                });

            await NavigationService.NavigateToStore();
        }

        public class Model
        {
            public Position Position { get; set; }

            public string Address { get; set; }

            public string Image { get; set; }

            public string Description { get; set; }

            public int IdStore { get; set; }

            public string PushToken { get; set; }

            public List<Product> Products { get; set; }

            public Models.StoreLocation Location { get; set; }
        }
    }
}
