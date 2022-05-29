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

        AddItemCommand = new AsyncCommand<Product>(AddItemCommandAsync);
        RemoveItemCommand = new AsyncCommand<Product>(RemoveItemCommandAsync);
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

    public ICommand RemoveItemCommand { get; set; }

    public ICommand AddItemCommand { get; set; }

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

        return base.OnAppearingAsync();
    }

    public override Task OnDisappearingAsync()
    {
        return base.OnDisappearingAsync();
        //Hub.Unsubscribe<OrderProduct>();
    }

    private Task AddItemCommandAsync(Product product)
    {
        listCartProducts.Add(OrderProduct.Parse(product));
        CountItems = listCartProducts.Count;
        return Task.CompletedTask;
    }
    
    private Task RemoveItemCommandAsync(Product product)
    {
        var itemforRemove = listCartProducts.FirstOrDefault(x => x.Id == product.Id);
        if (itemforRemove == null)
        {
            return Task.CompletedTask;
        }

        listCartProducts.Remove(itemforRemove);
        CountItems = listCartProducts.Count;

        return Task.CompletedTask;
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
