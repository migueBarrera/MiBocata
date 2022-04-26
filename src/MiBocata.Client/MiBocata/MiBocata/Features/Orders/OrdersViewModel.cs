using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mibocata.Core.Features.Orders;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using Models.Core;
using Models.Responses;

namespace MiBocata.Features.Orders
{
    public class OrdersViewModel : CoreViewModel
    {
        private readonly IOrderApi orderApi;
        private readonly IPreferencesService preferencesService;
        private readonly ILoggingService loggingService;
        private readonly ITaskHelperFactory taskHelperFactory;
        private IEnumerable<Order> order;
        private Client client;

        public OrdersViewModel(
            IPreferencesService preferencesService,
            ILoggingService loggingService,
            IRefitService refitService,
            ITaskHelperFactory taskHelperFactory)
        {
            this.orderApi = refitService.InitRefitInstance<IOrderApi>(isAutenticated: true);
            this.preferencesService = preferencesService;
            this.loggingService = loggingService;
            this.taskHelperFactory = taskHelperFactory;
        }

        public IEnumerable<Order> Orders
        {
            get => order;
            set => SetAndRaisePropertyChanged(ref order, value);
        }

        public override async Task InitializeAsync(object navigationData = null)
        {
            await base.InitializeAsync(navigationData);
            client = preferencesService.GetClient();
            Orders = await GetOrders();
        }

        private async Task<List<Order>> GetOrders()
        {
            var list = new List<Order>();
            var result = await taskHelperFactory.
                                    CreateInternetAccessViewModelInstance(loggingService, this).
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
