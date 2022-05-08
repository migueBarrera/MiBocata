using MiBocata.Businnes.Helpers;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Services.Interfaces;
using Models.Core;

namespace MiBocata.Businnes.Features.Stores;

internal class ChooseLocationService : IChooseLocationService
{
    private readonly IGeocodingService geocodingService;
    private readonly IGeolocationService geolocationService;
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

        //TODOawait miBocataNavigationService.NavigateToCreateStore();
    }
}
