using Mibocata.Core.Features.Refit;
using Mibocata.Core.Features.Stores;
using MvvmHelpers.Interfaces;

namespace MiBocata.Features.Stores;

public class StoresViewModel : CoreViewModel
{
    private readonly IStoreApi storeApi;
    private readonly IGeolocationService geolocationService;
    private readonly ISessionService sessionService;
    private readonly ILoggingService loggingService;
    private readonly ITaskHelperFactory taskHelperFactory;
    private Location userLocation;

    private IEnumerable<Store> stores;

    public IAsyncCommand<Store> GoToStoreDetailCommand { get; set; }

    public StoresViewModel(
        IGeolocationService geolocationService,
        ISessionService sessionService,
        ILoggingService loggingService,
        IRefitService refitService,
        ITaskHelperFactory taskHelperFactory)
    {
        this.geolocationService = geolocationService;
        this.sessionService = sessionService;
        this.loggingService = loggingService;
        this.taskHelperFactory = taskHelperFactory;
        this.storeApi = refitService.InitRefitInstance<IStoreApi>(isAutenticated: false);

        GoToStoreDetailCommand = new AsyncCommand<Store>((store) => GoToStoreAsync(store));
    }

    public Location UserLocation { get => userLocation; set => SetAndRaisePropertyChanged(ref userLocation, value); }

    public IEnumerable<Store> Stores { get => stores; set => SetAndRaisePropertyChanged(ref stores, value); }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();
        await GetStores();

        MainThread.BeginInvokeOnMainThread(async () => 
        {
            UserLocation = await geolocationService.GetLastKnownLocationAsync();
        });

    }

    private async Task GetStores()
    {
        var result = await taskHelperFactory.
                                CreateInternetAccessViewModelInstance(loggingService, this).
                                TryExecuteAsync(
                                () => storeApi.GetAll());

        if (result)
        {
            Stores = result.Value.Select(x =>
                           new Store()
                           {
                               Name = x.Name,
                               StoreLocation = StoreLocationResponse.Parse(x.StoreLocation),
                               Id = x.Id,
                               Image = x.Image,
                               Products = x.Products?.Select(pr => ProductsResponse.Parse(pr)).ToList(),
                           })?.ToList();
        }
    }

    public async Task GoToStoreAsync(Store store)
    {
        sessionService.Save(
            "KEY_SESSION_STORE",
            store);

        await Shell.Current.GoToAsync($"{nameof(StoreDetailPage)}");
    }
}
