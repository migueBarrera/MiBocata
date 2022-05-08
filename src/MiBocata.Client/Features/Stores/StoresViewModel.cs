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

    private IEnumerable<Model> stores;

    public IAsyncCommand<Model> GoToStoreDetailCommand { get; set; }

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

        GoToStoreDetailCommand = new AsyncCommand<Model>((store) => GoToStoreAsync(store));
    }

    public Location UserLocation { get => userLocation; set => SetAndRaisePropertyChanged(ref userLocation, value); }

    public IEnumerable<Model> Stores { get => stores; set => SetAndRaisePropertyChanged(ref stores, value); }

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
                           new Model()
                           {
                               //Position = new Position(x.StoreLocation.Latitude, x.StoreLocation.Longitude),
                               Description = x.Name,
                               Location = StoreLocationResponse.Parse(x.StoreLocation),
                               IdStore = x.Id,
                               Image = x.Image,
                               Products = x.Products?.Select(pr => ProductsResponse.Parse(pr)).ToList(),
                           })?.ToList();
        }
    }

    public async Task GoToStoreAsync(Model model)
    {
        var store = Stores.Where(x => x.IdStore == model.IdStore).FirstOrDefault();
        sessionService.Save(
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

        await Shell.Current.GoToAsync($"{nameof(StoreDetailPage)}");
    }

    public class Model
    {
       // public Position Position { get; set; }

        public string Address { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public int IdStore { get; set; }

        public string PushToken { get; set; }

        public List<Product> Products { get; set; }

        public StoreLocation Location { get; set; }
    }
}
