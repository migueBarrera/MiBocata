using System.Collections.Generic;
using System.Threading.Tasks;
using MiBocata.Framework;
using MiBocata.Services.API.Interfaces;
using Models;

namespace MiBocata.Features.Orders
{
    public class OrdersViewModel : BaseViewModel
    {
        private readonly IOrderApi orderApi;
        private IEnumerable<Order> order;
        private Client client;

        public OrdersViewModel()
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
                list = result.Value;
            }

            return list;
        }
    }
}
