using MiBocata.Features.Cart;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MiBocata.Features.Stores;

public class StoreDetailViewModel : CoreViewModel
{
    private Store store;
    private int countItems;
    private ObservableCollection<OrderProduct> listCartProducts;
    private readonly IPreferencesService preferencesService;
    private readonly ISessionService sessionService;
    private readonly IDialogService dialogService;

    public StoreDetailViewModel(
        IPreferencesService preferencesService,
        ISessionService sessionService,
        IDialogService dialogService)
    {
        listCartProducts = new ObservableCollection<OrderProduct>();
        this.preferencesService = preferencesService;
        this.sessionService = sessionService;
        this.dialogService = dialogService;

        AddRemoveItemCommand = new AsyncCommand<Product>(AddRemoveItemCommandAsync);

        GoToCartCommand = new AsyncCommand(async () => await GoToCartCommandAsync());
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

    public ICommand AddRemoveItemCommand { get; set; }

    public ICommand GoToCartCommand { get; set; }

    public override Task OnAppearingAsync()
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

        return base.OnAppearingAsync();
    }

    public override Task OnDisappearingAsync()
    {
        return base.OnDisappearingAsync();
        //Hub.Unsubscribe<OrderProduct>();
    }

    private async Task AddRemoveItemCommandAsync(Product product)
    {
        sessionService.Save("AddProduct", product);
        await Shell.Current.GoToAsync(nameof(AddProductPage));
        //await App.De.Resolve<INavigationService>().NavigateToPopupAsync<AddProductViewModel>(product, false);
    }

    private async Task GoToCartCommandAsync()
    {
        if (!preferencesService.IsLogged())
        {
            //await navigationService.NavigateToLogIn();
            return;
        }

        if (listCartProducts.Count == 0)
        {
            await dialogService.ShowAlertAsync(string.Empty, "Añade algun elemento al carrito");
            return;
        }

        sessionService.Save("ListCartProducts", listCartProducts);
        await Shell.Current.GoToAsync($"{nameof(CartPage)}");
    }

    private void AddProduct(OrderProduct product)
    {
        listCartProducts.Add(product);
        CountItems = listCartProducts.Count;
    }
}
