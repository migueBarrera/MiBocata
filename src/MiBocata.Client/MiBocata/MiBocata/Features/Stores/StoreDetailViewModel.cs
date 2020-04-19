using MiBocata.Framework;
using MiBocata.Services.NavigationService;
using Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MiBocata.Features.Stores
{
    public class StoreDetailViewModel : BaseViewModel
    {
        private Store store;
        private int countItems;
        private ObservableCollection<OrderProduct> listCartProducts;

        public StoreDetailViewModel()
        {
            listCartProducts = new ObservableCollection<OrderProduct>();
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

        public ICommand AddRemoveItemCommand => new Command<Product>(AddRemoveItemCommandAsync);

        public ICommand GoToCartCommand => new AsyncCommand(async () => await GoToCartCommandAsync());

        public override Task InitializeAsync(object navigationData)
        {
            Store = SessionService.Get<Store>("KEY_SESSION_STORE");
            listCartProducts = SessionService.Get<ObservableCollection<OrderProduct>>("ListCartProducts");
            if (listCartProducts == null)
            {
                listCartProducts = new ObservableCollection<OrderProduct>();
            }

            CountItems = listCartProducts.Count;

            Hub.Subscribe<OrderProduct>(product =>
            {
                AddProduct(product);
            });

            return base.InitializeAsync(navigationData);
        }

        public override Task UnitializeAsync(object navigationData = null)
        {
            Hub.Unsubscribe<OrderProduct>();
            return base.UnitializeAsync(navigationData);
        }

        private async void AddRemoveItemCommandAsync(Product product)
        {
            await Locator.Resolve<INavigationService>().NavigateToPopupAsync<AddProductViewModel>(product, false);
        }

        private async Task GoToCartCommandAsync()
        {
            if (!PreferencesService.IsLogged())
            {
                await NavigationService.NavigateToLogIn();
                return;
            }

            if (listCartProducts.Count == 0)
            {
                await DialogService.ShowAlertAsync(string.Empty, "Añade algun elemento al carrito");
                return;
            }

            SessionService.Save("ListCartProducts", listCartProducts);
            await NavigationService.NavigateToCart();
        }

        private void AddProduct(OrderProduct product)
        {
            listCartProducts.Add(product);
            CountItems = listCartProducts.Count;
        }
    }
}
