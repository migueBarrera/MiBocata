using System.Collections.ObjectModel;
using System.Windows.Input;
using Mibocata.Core.Features.Orders;
using Mibocata.Core.Features.Refit;
using Models.Requests;


namespace MiBocata.Features.Cart;

public class CartViewModel : CoreViewModel
{
    private readonly IPreferencesService preferencesService;
    private readonly ISessionService sessionService;
    private readonly ILoggingService loggingService;
    private readonly ITaskHelperFactory taskHelperFactory;
    private readonly IOrderApi orderApi;
    private Store store;
    private TimeSpan pickupTime;
    private Models.Core.Client client;
    private double amount;
    private ObservableCollection<OrderProduct> listCartProducts;

    public CartViewModel(
        IPreferencesService preferencesService,
        ISessionService sessionService,
        ILoggingService loggingService,
        IDialogService dialogService,
        IRefitService refitService,
        ITaskHelperFactory taskHelperFactory)
    {
        this.preferencesService = preferencesService;
        this.sessionService = sessionService;
        this.loggingService = loggingService;
        this.taskHelperFactory = taskHelperFactory;
        this.orderApi = refitService.InitRefitInstance<IOrderApi>(isAutenticated: true);


        MakeOrderCommand = new AsyncCommand(async () => await MakeOrderCommandAsync());
        RemoveItemCommand = new AsyncCommand<OrderProduct>(RemoveItemCommandExecute);
}

    public Store Store
    {
        get => store;
        set => SetAndRaisePropertyChanged(ref store, value);
    }

    public TimeSpan PickupTime
    {
        get => pickupTime;
        set => SetAndRaisePropertyChanged(ref pickupTime, value);
    }

    public double Amount
    {
        get => amount;
        set => SetAndRaisePropertyChanged(ref amount, value);
    }

    public ObservableCollection<OrderProduct> ListCartProducts
    {
        get => listCartProducts;
        set => SetAndRaisePropertyChanged(ref listCartProducts, value);
    }

    public ICommand MakeOrderCommand {get; set;}

    public ICommand RemoveItemCommand { get; set; }

    private Task RemoveItemCommandExecute(OrderProduct orderProduct)
    {
        var data = ListCartProducts.Where(x => x.IdOriginalProduct == orderProduct.IdOriginalProduct).FirstOrDefault();
        var index = ListCartProducts.IndexOf(data);
        ListCartProducts.RemoveAt(index);
        CalcAmount();
        RefresView();

        return Task.CompletedTask;
    }

    public override Task OnAppearingAsync()
    {
        Store = sessionService.Get<Store>("KEY_SESSION_STORE");
        ListCartProducts = sessionService.Get<ObservableCollection<OrderProduct>>("ListCartProducts");
        PickupTime = DateTime.UtcNow.AddMinutes(15).TimeOfDay;
        CalcAmount();
        client = preferencesService.GetClient();

        return base.OnAppearingAsync();
    }

    private void CalcAmount()
    {
        double a = 0;
        foreach (var item in ListCartProducts)
        {
            a += item.UnitPrice;
        }

        Amount = a;
    }

    private async Task MakeOrderCommandAsync()
    {
        if (IsBusy)
        {
            return;
        }

        if (ListCartProducts.Count == 0)
        {
            return;
        }

        var order = new Order()
        {
            ClientId = client.Id,
            OrderProducts = ListCartProducts,
            Amount = this.Amount,
            PickupTime = DateTime.Today.ToUniversalTime().Add(this.PickupTime).ToUniversalTime(),
            StoreId = Store.Id,
            State = store.AutoAccept ? OrderStates.AUTOACCEPTED : OrderStates.STARTED,
        };

        var result = await taskHelperFactory.
                                CreateInternetAccessViewModelInstance(loggingService, this).
                                TryExecuteAsync(
                                () => orderApi.Create(CreateOrderRequest.Parse(order)));

        if (result)
        {
            sessionService.Save("ListCartProducts", new ObservableCollection<OrderProduct>());
            await Shell.Current.GoToAsync("///StoresPage");
        }
    }

    public void RefresView()
    {
        CalcAmount();
        sessionService.Save("ListCartProducts", ListCartProducts);
    }
}
