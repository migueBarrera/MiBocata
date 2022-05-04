using Mibocata.Core.Features.Refit;
using Mibocata.Core.Features.Stores;
using MiBocata.Services.NavigationService;

namespace MiBocata.Features.Stores;

public class StoresViewModel : CoreViewModel
{
    private readonly IStoreApi storeApi;
    private readonly IGeolocationService geolocationService;
    private readonly IMiBocataNavigationService navigationService;
    private readonly ISessionService sessionService;
    private readonly ILoggingService loggingService;
    private readonly ITaskHelperFactory taskHelperFactory;
    private Location userLocation;

    private IEnumerable<Model> stores;

    public StoresViewModel(
        IGeolocationService geolocationService,
        IMiBocataNavigationService navigationService,
        ISessionService sessionService,
        ILoggingService loggingService,
        IRefitService refitService,
        ITaskHelperFactory taskHelperFactory)
    {
        this.geolocationService = geolocationService;
        this.navigationService = navigationService;
        this.sessionService = sessionService;
        this.loggingService = loggingService;
        this.taskHelperFactory = taskHelperFactory;
        this.storeApi = refitService.InitRefitInstance<IStoreApi>(isAutenticated: false);
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

    public async Task GoToStore(Model model)
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

        await navigationService.NavigateToStore();
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
