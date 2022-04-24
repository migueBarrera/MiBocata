using System.Threading.Tasks;
using System.Windows.Input;
using Mibocata.Core.Framework;
using MiBocata.Businnes.Features.Configuration;
using MiBocata.Businnes.Features.LogIn;
using MiBocata.Businnes.Features.Products;
using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Services.Commons.Navigation;
using MiBocata.Businnes.Services.Commons.Preferences;
using Models.Core;

namespace MiBocata.Businnes.Features.Stores
{
    public class StoreViewModel : CoreViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IPreferencesService preferencesService;
        private Store store;

        public StoreViewModel(
             INavigationService navigationService,
             IPreferencesService preferencesService)
        {
            this.navigationService = navigationService;
            this.preferencesService = preferencesService;
        }

        public Store Store
        {
            get => store;
            set
            {
                SetAndRaisePropertyChanged(ref store, value);
            }
        }

        public ICommand ConfigCommand => new AsyncCommand(_ => ConfigCommandAsync());

        public ICommand ProductsCommand => new AsyncCommand(_ => ProductsCommandAsync());

        public ICommand CloseSessionCommand => new AsyncCommand(_ => CloseSessionCommandAsync());

        public override Task InitializeAsync(object navigationData)
        {
            Store = preferencesService.GetStore();
            return base.InitializeAsync(navigationData);
        }

        private async Task CloseSessionCommandAsync()
        {
            preferencesService.SetUser(null);
            preferencesService.SetStore(null);
            await navigationService.NavigateToAsync<LogInViewModel>(clearStack: true);
        }

        private async Task ConfigCommandAsync() => await navigationService.NavigateToAsync<ConfigurationViewModel>();

        private async Task ProductsCommandAsync() => await navigationService.NavigateToAsync<ProductsViewModel>();
    }
}
