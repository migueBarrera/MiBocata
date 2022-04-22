using Mibocata.Core.Features.Refit;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Framework;
using MiBocata.Services.NavigationService;
using MiBocata.Services.PreferencesService;
using Models.Core;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiBocata.Features.Stores
{
    public class AddProductViewModel : BaseViewModel
    {
        private OrderProduct product;

        public AddProductViewModel(
            IMiBocataNavigationService navigationService,
            IPreferencesService preferencesService,
            ISessionService sessionService,
            ILoggingService loggingService,
            IDialogService dialogService,
            IConnectivityService connectivityService,
            IRefitService refitService,
            ITaskHelperFactory taskHelperFactory,
            IKeyboardService keyboardService) 
            : base(
                  navigationService,
                  preferencesService,
                  sessionService,
                  loggingService,
                  dialogService,
                  connectivityService,
                  refitService,
                  taskHelperFactory,
                  keyboardService)
        {
        }

        public OrderProduct Product
        {
            get => product;
            set => SetAndRaisePropertyChanged(ref product, value);
        }

        public ICommand AddCommand => new AsyncCommand(AddCommandExecute);

        private async Task AddCommandExecute()
        {
            await Hub.PublishAsync(Product);
            await PopupNavigation.Instance.PopAllAsync();
        }

        public override Task InitializeAsync(object navigationData = null)
        {
            var p = navigationData as Product;
            if (p != null)
            {
                Product = OrderProduct.Parse(p);
            }

            return base.InitializeAsync(navigationData);
        }
    }
}
