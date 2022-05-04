using MiBocata.Services.NavigationService;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MiBocata.Features.Stores;

public class StoreDetailViewModel : CoreViewModel
{
    private Store store;
    private int countItems;
    private ObservableCollection<OrderProduct> listCartProducts;
    private readonly IMiBocataNavigationService navigationService;
    private readonly IPreferencesService preferencesService;
    private readonly ISessionService sessionService;
    private readonly IDialogService dialogService;
   // private Hub Hub = Hub.Default;

    public StoreDetailViewModel(
        IMiBocataNavigationService navigationService,
        IPreferencesService preferencesService,
        ISessionService sessionService,
        IDialogService dialogService)
    {
        listCartProducts = new ObservableCollection<OrderProduct>();
        this.navigationService = navigationService;
        this.preferencesService = preferencesService;
        this.sessionService = sessionService;
        this.dialogService = dialogService;
    }

    public Store Store
    {
        get => store;
        set => SetAndRaisePropertyChanged(ref store, value);
    }
    
    public int CountItems
    {
        get => countItems;
        set => SetAndRaisePropertyChanged(ref countItems, value);
    }

    public ICommand AddRemoveItemCommand => new AsyncCommand<Product>(AddRemoveItemCommandAsync);

    public ICommand GoToCartCommand => new AsyncCommand(async () => await GoToCartCommandAsync());

    public override Task InitializeAsync(object navigationData)
    {
        Store = sessionService.Get<Store>("KEY_SESSION_STORE");
        listCartProducts = sessionService.Get<ObservableCollection<OrderProduct>>("ListCartProducts");
        if (listCartProducts == null)
        {
            listCartProducts = new ObservableCollection<OrderProduct>();
        }

        CountItems = listCartProducts.Count;

        //Hub.Subscribe<OrderProduct>(product =>
        //{
        //    AddProduct(product);
        //});

        return base.InitializeAsync(navigationData);
    }

    public override Task UnitializeAsync(object navigationData = null)
    {
        //Hub.Unsubscribe<OrderProduct>();
        return base.UnitializeAsync(navigationData);
    }

    private async Task AddRemoveItemCommandAsync(Product product)
    {
        //await App.De.Resolve<INavigationService>().NavigateToPopupAsync<AddProductViewModel>(product, false);
    }

    private async Task GoToCartCommandAsync()
    {
        if (!preferencesService.IsLogged())
        {
            await navigationService.NavigateToLogIn();
            return;
        }

        if (listCartProducts.Count == 0)
        {
            await dialogService.ShowAlertAsync(string.Empty, "Añade algun elemento al carrito");
            return;
        }

        sessionService.Save("ListCartProducts", listCartProducts);
        await navigationService.NavigateToCart();
    }

    private void AddProduct(OrderProduct product)
    {
        listCartProducts.Add(product);
        CountItems = listCartProducts.Count;
    }
}
