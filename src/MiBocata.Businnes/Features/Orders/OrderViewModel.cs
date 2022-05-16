using System.Collections.ObjectModel;
using System.Windows.Input;
using Mibocata.Core.Features.Orders;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Services.Interfaces;
using Models.Core;
using Mibocata.Core.Framework;
using Models.Responses;

namespace MiBocata.Businnes.Features.Orders
{
    public class OrderViewModel : CoreViewModel
    {
        private readonly IOrderApi orderApi;
        private readonly IPreferencesService preferencesService;
        private readonly ITaskHelperFactory taskHelperFactory;
        private readonly ILoggingService loggingService;
        private readonly IDialogService dialogService;

        private ObservableCollection<Order> orders;

        private Store store;

        public OrderViewModel(
           IRefitService refitService,
           IPreferencesService preferencesService,
           ITaskHelperFactory taskHelperFactory,
           ILoggingService loggingService,
           IDialogService dialogService)
        {
            this.orderApi = refitService.InitRefitInstance<IOrderApi>(isAutenticated: true);
            this.preferencesService = preferencesService;
            this.taskHelperFactory = taskHelperFactory;
            this.loggingService = loggingService;
            this.dialogService = dialogService;
        }

        public ObservableCollection<Order> Orders
        {
            get => orders;
            set => SetAndRaisePropertyChanged(ref orders, value);
        }

        public ICommand RefreshCommand => new AsyncCommand(() => RefreshCommandAsync());

        public ICommand OpenChatCommand => new AsyncCommand<Order>((order) => OpenChatCommandAsync(order));

        public ICommand CancelOrderCommand => new AsyncCommand<Order>((order) => CancelOrderCommandExecute(order));

        public ICommand AcceptOrderCommand => new AsyncCommand<Order>((order) => AcceptOrderCommandExecute(order));

        public override async Task OnAppearingAsync()
        {
            await base.OnAppearingAsync();
            store = preferencesService.GetStore();
            await RefreshCommandAsync();
        }

        private async Task OpenChatCommandAsync(Order order)
        {
            if (string.IsNullOrWhiteSpace(order?.Client?.Phone))
            {
                return;
            }

            var uriString = $"whatsapp://send?phone=+{34}{order.Client.Phone}";

            await Launcher.TryOpenAsync(uriString);
        }

        private async Task CancelOrderCommandExecute(Order order)
        {
            await UpdateOrder(order, OrderStates.REJECTED);
        }

        private async Task AcceptOrderCommandExecute(Order order)
        {
            await UpdateOrder(order, OrderStates.ACCEPTED);
        }

        private async Task RefreshCommandAsync(bool force = false)
        {
            if (IsBusy)
            {
                if (!force)
                {
                    return;
                }
            }

            await taskHelperFactory.CreateInternetAccessViewModelInstance(loggingService, this).
                TryExecuteAsync(() => GetOrders());
        }

        private async Task GetOrders()
        {
            var list = await orderApi.GetAllByStore(store.Id);
            Orders = new ObservableCollection<Order>(list.Select((x) => OrdersResponse.Parse(x)));
        }

        private async Task UpdateOrder(Order order, OrderStates states)
        {
            order.State = states;

            var response = await taskHelperFactory.CreateInternetAccessViewModelInstance(loggingService, this).
                                    TryExecuteAsync(() => orderApi.Update(order.Id, new Models.Requests.UpdateOrderRequest()
                                    {
                                        //order todo
                                    }));
            if (response)
            {
                await RefreshCommandAsync(force: true);
            }
            else
            {
                await dialogService.ShowMessage("Ocurrio un error", string.Empty);
            }
        }
    }
}
