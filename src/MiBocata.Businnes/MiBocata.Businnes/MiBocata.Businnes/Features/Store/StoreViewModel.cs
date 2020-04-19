using MiBocata.Businnes.Features.Configuration;
using MiBocata.Businnes.Features.LogIn;
using MiBocata.Businnes.Features.Products;
using MiBocata.Businnes.Framework;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiBocata.Businnes.Features.Store
{
    public class StoreViewModel : BaseViewModel
    {
        private Models.Store store;

        public Models.Store Store
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
            Store = PreferencesService.GetStore();
            return base.InitializeAsync(navigationData);
        }

        private async Task CloseSessionCommandAsync()
        {
            PreferencesService.SetUser(null);
            PreferencesService.SetStore(null);
            await NavigationService.NavigateToAsync<LogInViewModel>(clearStack: true);
        }

        private async Task ConfigCommandAsync() => await NavigationService.NavigateToAsync<ConfigurationViewModel>();

        private async Task ProductsCommandAsync() => await NavigationService.NavigateToAsync<ProductsViewModel>();
    }
}
