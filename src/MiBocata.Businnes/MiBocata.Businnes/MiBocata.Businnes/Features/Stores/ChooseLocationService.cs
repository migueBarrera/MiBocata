using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiBocata.Businnes.Helpers;
using MiBocata.Businnes.Services.Commons.Navigation;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Services.Interfaces;
using Models.Core;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace MiBocata.Businnes.Features.Stores
{
    internal class ChooseLocationService : IChooseLocationService
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

        public ChooseLocationService(
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

        public Task<Location> GetLastKnownLocationAsync()
        {
            return geolocationService.GetLastKnownLocationAsync();
        }

        public async Task GetPlaceMarkAndContinue(IEnumerable<Model> locations)
        {
            if (locations == null || !locations.Any())
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
    }
}
