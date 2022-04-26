using Mibocata.Core.Extensions;
using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Framework;
using MiBocata.Services.NavigationService;
using Models.Core;
using PubSub;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MiBocata.Features.Stores
{
    public class StoreDetailViewModel : CoreViewModel
    {
        private Store store;
        private int countItems;
        private ObservableCollection<OrderProduct> listCartProducts;
        private readonly IMiBocataNavigationService navigationService;
        private readonly IPreferencesService preferencesService;
        private readonly ISessionService sessionService;
        private readonly IDialogService dialogService;
        private Hub Hub = Hub.Default;

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

        public ICommand AddRemoveItemCommand => new Command<Product>(AddRemoveItemCommandAsync);

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
            await App.Current.DependencyService.Resolve<INavigationService>().NavigateToPopupAsync<AddProductViewModel>(product, false);
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
}
