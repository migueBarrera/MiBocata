using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mibocata.Core.Features.Orders;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Framework;
using MiBocata.Services.NavigationService;
using MiBocata.Services.NotificationService;
using MiBocata.Services.PreferencesService;
using Models.Core;
using Models.Responses;

namespace MiBocata.Features.Orders
{
    public class OrdersViewModel : BaseViewModel
    {
        private readonly IOrderApi orderApi;
        private IEnumerable<Order> order;
        private Client client;

        public OrdersViewModel(
            INotificationService notificationService,
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
            this.orderApi = RefitService.InitRefitInstance<IOrderApi>(isAutenticated: true);
        }

        public IEnumerable<Order> Orders
        {
            get => order;
            set => SetAndRaisePropertyChanged(ref order, value);
        }

        public override async Task InitializeAsync(object navigationData = null)
        {
            await base.InitializeAsync(navigationData);
            client = PreferencesService.GetUser();
            Orders = await GetOrders();
        }

        private async Task<List<Order>> GetOrders()
        {
            var list = new List<Order>();
            var result = await TaskHelperFactory.
                                    CreateInternetAccessViewModelInstance(LoggingService, this).
                                    TryExecuteAsync(
                                    () => orderApi.GetAllByClient(client.Id));

            if (result)
            {
                list = result
                    .Value
                    .Select(o => OrdersResponse.Parse(o))
                    .ToList();
            }

            return list;
        }
    }
}
