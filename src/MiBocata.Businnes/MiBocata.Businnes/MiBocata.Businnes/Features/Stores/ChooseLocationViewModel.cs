using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Helpers;
using MiBocata.Businnes.Services.Commons.Navigation;
using MiBocata.Businnes.Services.Commons.Preferences;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using Models.Core;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace MiBocata.Businnes.Features.Stores
{
    public class ChooseLocationViewModel : CoreViewModel
    {
        private readonly IGeocodingService geocodingService;
        private readonly IGeolocationService geolocationService;
        private readonly INavigationService navigationService;
        private readonly IMiBocataNavigationService miBocataNavigationService;
        private readonly IPreferencesService preferencesService;
        private readonly ISessionService sessionService;
        private readonly ILoggingService loggingService;
        private readonly IDialogService dialogService;
        private readonly IConnectivityService connectivityService;
        private readonly ITaskHelper taskHelper;
        private readonly IRefitService refitService;
        private readonly ITaskHelperFactory taskHelperFactory;
        private Location userLocation;
        private IEnumerable<Model> locations;

        public ChooseLocationViewModel(
             IGeocodingService geocodingService,
             IGeolocationService geolocationService,
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
        {
            this.geocodingService = geocodingService;
            this.geolocationService = geolocationService;
            this.navigationService = navigationService;
            this.miBocataNavigationService = miBocataNavigationService;
            this.preferencesService = preferencesService;
            this.sessionService = sessionService;
            this.loggingService = loggingService;
            this.dialogService = dialogService;
            this.connectivityService = connectivityService;
            this.taskHelper = taskHelper;
            this.refitService = refitService;
            this.taskHelperFactory = taskHelperFactory;
        }

        public Location UserLocation { get => userLocation; set => SetAndRaisePropertyChanged(ref userLocation, value); }

        public IEnumerable<Model> Locations { get => locations; set => SetAndRaisePropertyChanged(ref locations, value); }

        public ICommand NextCommand => new AsyncCommand(_ => NextCommandAsync());

        public override async Task InitializeAsync(object navigationData)
        {
            UserLocation = await geolocationService.GetLastKnownLocationAsync();

            await base.InitializeAsync(navigationData);
        }

        private async Task NextCommandAsync()
        {
            if (Locations == null || Locations.Count() == 0)
            {
                // Todo show error
                return;
            }

            var latitude = locations.First().Position.Latitude;
            var longitude = locations.First().Position.Longitude;

            var placemark = await geocodingService.GetPlaceMark(latitude, longitude);

            var store = new Store()
            {
                StoreLocation = PlacemarkLocationMapper.Mapper(placemark, latitude, longitude),
            };

            sessionService.Save(
                nameof(Store),
                store);

            await miBocataNavigationService.NavigateToCreateStore();
        }

        public void UserSelectedPosition(Position position)
        {
            Locations = new List<Model>()
            {
                new Model()
                {
                    Position = new Position(position.Latitude, position.Longitude),
                    Description = "Tu tienda",
                    Address = string.Empty,
                },
            };
        }

        public class Model
        {
            public Position Position { get; set; }

            public string Address { get; set; }

            public string Description { get; set; }
        }
    }
}
