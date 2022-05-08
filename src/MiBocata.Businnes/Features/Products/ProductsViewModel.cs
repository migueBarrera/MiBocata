using System.Collections.ObjectModel;
using System.Windows.Input;
using MiBocata.Businnes.Services.Commons.Navigation;
using Mibocata.Core.Features.Products;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using Models.Core;
using Models.Responses;

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

        public ICommand NewProductCommand => new AsyncCommand(() => NewProductCommandAsync());

        public ObservableCollection<Product> Products
        {
            get => products;
            set
                {
                SetAndRaisePropertyChanged(ref products, value);
                HasProducts = Products?.Any() ?? false;
            }
        }

        public override async Task OnAppearingAsync()
        {
            await base.OnAppearingAsync();
            store = preferencesService.GetStore();
            await GetProducts();
        }

        private async Task GetProducts()
        {
            var result = await taskHelperFactory.CreateInternetAccessViewModelInstance(loggingService, this).
                                TryExecuteAsync(() => productApi.GetByStore(store.Id));

            if (result)
            {
                Products = new ObservableCollection<Product>(result.Value.Select((x) => ProductsResponse.Parse(x)));
            }
        }

        private async Task NewProductCommandAsync()
        {
            await navigationService.NavigateToAsync<NewProductViewModel>();
        }
    }
}
