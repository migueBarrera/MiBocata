using Mibocata.Core.Features.Refit;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Businnes.Features.Configuration;
using MiBocata.Businnes.Features.LogIn;
using MiBocata.Businnes.Features.Products;
using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Services.Commons.Navigation;
using MiBocata.Businnes.Services.Commons.Preferences;
using Models.Core;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiBocata.Businnes.Features.Stores
{
    public class StoreViewModel : BaseViewModel
    {
        private Store store;

        public StoreViewModel(
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
