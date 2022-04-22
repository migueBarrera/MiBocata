using Mibocata.Core.Features.Products;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Services.Commons.Navigation;
using MiBocata.Businnes.Services.Commons.Preferences;
using MiBocata.Businnes.Services.Commons.Products;
using Models.Core;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiBocata.Businnes.Features.Products
{
    public class ProductsViewModel : BaseViewModel
    {
        private bool hasProducts;
        private Store store;

        private readonly IProductsService productsService;
        private readonly IProductApi productApi;

        private ObservableCollection<Product> products;

        public ProductsViewModel(
            IProductsService productsService,
            INavigationService navigationService,
            IMiBocataNavigationService miBocataNavigationService,
            IPreferencesService preferencesService,
            ISessionService sessionService,
            ILoggingService loggingService,
            IDialogService dialogService,
            IConnectivityService connectivityService,
            ITaskHelper taskHelper,
            IRefitService refitService,
            ITaskHelperFactory taskHelperFactory)
            : base(
                  navigationService,
                  miBocataNavigationService,
                  preferencesService,
                  sessionService,
                  loggingService,
                  dialogService,
                  connectivityService,
                  taskHelper,
                  refitService,
                  taskHelperFactory)
        {
            this.productsService = productsService;
            this.productApi = RefitService.InitRefitInstance<IProductApi>(isAutenticated: true);
        }

        public bool HasProducts
        {
            get => hasProducts;
            set => SetAndRaisePropertyChanged(ref hasProducts, value);
        }

        public ICommand NewProductCommand => new AsyncCommand(_ => NewProductCommandAsync());

        public ObservableCollection<Product> Products
        {
            get => products;
            set
                {
                SetAndRaisePropertyChanged(ref products, value);
                HasProducts = Products?.Count != 0;
            }
        }

        public override async Task InitializeAsync(object navigationData)
        {
            await base.InitializeAsync(navigationData);
            store = PreferencesService.GetStore();
            await GetProducts();
        }

        private async Task GetProducts()
        {
            var result = await TaskHelperFactory.CreateInternetAccessViewModelInstance(LoggingService, this).
                                TryExecuteAsync(() => productApi.GetByStore(store.Id));

            if (result)
            {
                Products = new ObservableCollection<Product>(result.Value.Select((x) => new Product()));//todo
            }
        }

        private async Task NewProductCommandAsync()
        {
            await NavigationService.NavigateToAsync<NewProductViewModel>();
        }
    }
}
