using Mibocata.Core.Features.Products;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Framework;
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
    public class ProductsViewModel : CoreViewModel
    {
        private bool hasProducts;
        private Store store;

        private readonly INavigationService navigationService;
        private readonly IPreferencesService preferencesService;
        private readonly ILoggingService loggingService;
        private readonly ITaskHelperFactory taskHelperFactory;
        private readonly IProductApi productApi;

        private ObservableCollection<Product> products;

        public ProductsViewModel(
            INavigationService navigationService,
            IPreferencesService preferencesService,
            ILoggingService loggingService,
            IRefitService refitService,
            ITaskHelperFactory taskHelperFactory)
        {
            this.navigationService = navigationService;
            this.preferencesService = preferencesService;
            this.loggingService = loggingService;
            this.taskHelperFactory = taskHelperFactory;
            this.productApi = refitService.InitRefitInstance<IProductApi>(isAutenticated: true);
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
            store = preferencesService.GetStore();
            await GetProducts();
        }

        private async Task GetProducts()
        {
            var result = await taskHelperFactory.CreateInternetAccessViewModelInstance(loggingService, this).
                                TryExecuteAsync(() => productApi.GetByStore(store.Id));

            if (result)
            {
                Products = new ObservableCollection<Product>(result.Value.Select((x) => new Product()));//todo
            }
        }

        private async Task NewProductCommandAsync()
        {
            await navigationService.NavigateToAsync<NewProductViewModel>();
        }
    }
}
