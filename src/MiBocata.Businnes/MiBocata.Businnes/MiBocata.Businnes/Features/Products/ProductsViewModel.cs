using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Services.API.Interfaces;
using MiBocata.Businnes.Services.Products;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiBocata.Businnes.Features.Products
{
    public class ProductsViewModel : BaseViewModel
    {
        private bool hasProducts;
        private Models.Store store;

        private readonly IProductsService productsService;
        private readonly IProductApi productApi;

        private ObservableCollection<Models.Product> products;

        public ProductsViewModel(IProductsService productsService)
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

        public ObservableCollection<Models.Product> Products
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
                Products = new ObservableCollection<Models.Product>(result.Value);
            }
        }

        private async Task NewProductCommandAsync()
        {
            await NavigationService.NavigateToAsync<NewProductViewModel>();
        }
    }
}
