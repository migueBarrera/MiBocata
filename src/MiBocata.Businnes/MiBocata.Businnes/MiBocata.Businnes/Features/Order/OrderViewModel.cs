using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Services.API.Interfaces;
using Models;
using Xamarin.Forms;

namespace MiBocata.Businnes.Features.Order
{
    public class OrderViewModel : BaseViewModel
    {
        private readonly IOrderApi orderApi;

        private ObservableCollection<Models.Order> orders;

        private Models.Store store;

        public OrderViewModel()
        {
            this.orderApi = RefitService.InitRefitInstance<IOrderApi>(isAutenticated: true);
        }

        public ObservableCollection<Models.Order> Orders
        {
            get => orders;
            set => SetAndRaisePropertyChanged(ref orders, value);
        }

        public ICommand RefreshCommand => new AsyncCommand(_ => RefreshCommandAsync());

        public ICommand OpenChatCommand => new Command<Models.Order>(OpenChatCommandAsync);

        public ICommand CancelOrderCommand => new Command<Models.Order>(CancelOrderCommandExecute);

        public ICommand AcceptOrderCommand => new Command<Models.Order>(AcceptOrderCommandExecute);

        public override async Task InitializeAsync(object navigationData)
        {
            await base.InitializeAsync(navigationData);
            store = PreferencesService.GetStore();
            await RefreshCommandAsync();
        }

        private async void OpenChatCommandAsync(Models.Order order)
        {
            if (string.IsNullOrWhiteSpace(order.Client.Phone))
            {
                return;
            }

            var uriString = $"whatsapp://send?phone=+{34}{order.Client.Phone}";

            await Xamarin.Essentials.Launcher.TryOpenAsync(uriString);
        }

        private async void CancelOrderCommandExecute(Models.Order order)
        {
            await UpdateOrder(order, OrderStates.REJECTED);
        }

        private async void AcceptOrderCommandExecute(Models.Order order)
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
            Orders = new ObservableCollection<Models.Order>(list);
        }

        private async Task UpdateOrder(Models.Order order, OrderStates states)
        {
            order.State = states;

            var response = await TaskHelperFactory.CreateInternetAccessViewModelInstance(LoggingService, this).
                                    TryExecuteAsync(() => orderApi.Update(order.Id, order));
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
