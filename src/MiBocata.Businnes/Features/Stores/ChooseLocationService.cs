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
        //At the moment, maps are not supported in .net maui. Click the button and a random location will be sent.

        var latitude = GetRandomNumber(37.18817, 43.26271);
        var longitude = GetRandomNumber(-5.84476, -0.98623);

        //if (locations == null || !locations.Any())
        //{
        //    // Todo show error
        //    return;
        //}

        //var latitude = locations.First().Position.Latitude;
        //var longitude = locations.First().Position.Longitude;

        var placemark = await geocodingService.GetPlaceMark(latitude, longitude);

        var store = new Store()
        {
            StoreLocation = PlacemarkLocationMapper.Mapper(placemark, latitude, longitude),
        };

        sessionService.Save(
            nameof(Store),
            store);

        await Shell.Current.GoToAsync($"///{nameof(CreateStorePage)}");
    }

    //TEMPORAL
    public double GetRandomNumber(double minimum, double maximum)
    {
        Random random = new Random();
        return random.NextDouble() * (maximum - minimum) + minimum;
    }
}
