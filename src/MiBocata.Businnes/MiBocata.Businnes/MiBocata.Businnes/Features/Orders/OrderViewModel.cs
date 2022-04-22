using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Mibocata.Core.Features.Orders;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Services.Commons.Navigation;
using MiBocata.Businnes.Services.Commons.Preferences;
using Models.Core;
using Xamarin.Forms;

namespace MiBocata.Businnes.Features.Orders
{
    public class OrderViewModel : BaseViewModel
    {
        private readonly IOrderApi orderApi;

        private ObservableCollection<Order> orders;

        private Store store;

        public OrderViewModel(
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
            orderApi = RefitService.InitRefitInstance<IOrderApi>(isAutenticated: true);
        }

        public ObservableCollection<Order> Orders
        {
            get => orders;
            set => SetAndRaisePropertyChanged(ref orders, value);
        }

        public ICommand RefreshCommand => new AsyncCommand(_ => RefreshCommandAsync());

        public ICommand OpenChatCommand => new Command<Order>(OpenChatCommandAsync);

        public ICommand CancelOrderCommand => new Command<Order>(CancelOrderCommandExecute);

        public ICommand AcceptOrderCommand => new Command<Order>(AcceptOrderCommandExecute);

        public override async Task InitializeAsync(object navigationData)
        {
            await base.InitializeAsync(navigationData);
            store = PreferencesService.GetStore();
            await RefreshCommandAsync();
        }

        private async void OpenChatCommandAsync(Order order)
        {
            if (string.IsNullOrWhiteSpace(order.Client.Phone))
            {
                return;
            }

            var uriString = $"whatsapp://send?phone=+{34}{order.Client.Phone}";

            await Xamarin.Essentials.Launcher.TryOpenAsync(uriString);
        }

        private async void CancelOrderCommandExecute(Order order)
        {
            await UpdateOrder(order, OrderStates.REJECTED);
        }

        private async void AcceptOrderCommandExecute(Order order)
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

            await TaskHelperFactory.CreateInternetAccessViewModelInstance(LoggingService, this).
                TryExecuteAsync(() => GetOrders());
        }

        private async Task GetOrders()
        {
            var list = await orderApi.GetAllByStore(store.Id);
            Orders = new ObservableCollection<Order>(list.Select((x) => new Order())); //todo mapper
        }

        private async Task UpdateOrder(Order order, OrderStates states)
        {
            order.State = states;

            var response = await TaskHelperFactory.CreateInternetAccessViewModelInstance(LoggingService, this).
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
                await DialogService.ShowMessage("Ocurrio un error", string.Empty);
            }
        }
    }
}
