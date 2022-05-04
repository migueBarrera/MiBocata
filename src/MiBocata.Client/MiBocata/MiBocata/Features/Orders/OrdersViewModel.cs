using Mibocata.Core.Features.Orders;
using Mibocata.Core.Features.Refit;
namespace MiBocata.Features.Orders;

public class OrdersViewModel : CoreViewModel
{
    private readonly IOrderApi orderApi;
    private readonly IPreferencesService preferencesService;
    private readonly ILoggingService loggingService;
    private readonly ITaskHelperFactory taskHelperFactory;
    private IEnumerable<Order> order;
    private Models.Core.Client client;

    public OrdersViewModel(
        IPreferencesService preferencesService,
        ILoggingService loggingService,
        IRefitService refitService,
        ITaskHelperFactory taskHelperFactory)
    {
        this.orderApi = refitService.InitRefitInstance<IOrderApi>(isAutenticated: true);
        this.preferencesService = preferencesService;
        this.loggingService = loggingService;
        this.taskHelperFactory = taskHelperFactory;
    }

    public IEnumerable<Order> Orders
    {
        get => order;
        set => SetAndRaisePropertyChanged(ref order, value);
    }

    public override async Task InitializeAsync(object navigationData = null)
    {
        await base.InitializeAsync(navigationData);
        client = preferencesService.GetClient();
        Orders = await GetOrders();
    }

    private async Task<List<Order>> GetOrders()
    {
        var list = new List<Order>();
        var result = await taskHelperFactory.
                                CreateInternetAccessViewModelInstance(loggingService, this).
                                TryExecuteAsync(
                                () => orderApi.GetAllByClient(client.Id));

        if (result)
        {
            list = result
                .Value
                .Select(o => OrdersResponse.Parse(o))
                .ToList();
        }

        return list;
    }
}
