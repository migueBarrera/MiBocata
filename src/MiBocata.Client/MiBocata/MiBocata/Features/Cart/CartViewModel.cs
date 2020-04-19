using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MiBocata.Framework;
using MiBocata.Services.API.Interfaces;
using MiBocata.Services.NotificationService;
using Models;
using Xamarin.Forms;

namespace MiBocata.Features.Cart
{
    public class CartViewModel : BaseViewModel
    {
        private readonly INotificationService notificationService;
        private readonly IOrderApi orderApi;
        private Store store;
        private TimeSpan pickupTime;
        private Client client;
        private double amount;
        private ObservableCollection<OrderProduct> listCartProducts;

        public CartViewModel(INotificationService notificationService)
        {
            this.notificationService = notificationService;
            this.orderApi = RefitService.InitRefitInstance<IOrderApi>(isAutenticated: true);
        }

        public Store Store
        {
            get => store;
            set => SetAndRaisePropertyChanged(ref store, value);
        }

        public TimeSpan PickupTime
        {
            get => pickupTime;
            set => SetAndRaisePropertyChanged(ref pickupTime, value);
        }

        public double Amount
        {
            get => amount;
            set => SetAndRaisePropertyChanged(ref amount, value);
        }

        public ObservableCollection<OrderProduct> ListCartProducts
        {
            get => listCartProducts;
            set => SetAndRaisePropertyChanged(ref listCartProducts, value);
        }

        public ICommand MakeOrderCommand => new AsyncCommand(async () => await MakeOrderCommandAsync());

        public ICommand RemoveItemCommand => new Command<OrderProduct>(RemoveItemCommandExecute);

        private void RemoveItemCommandExecute(OrderProduct orderProduct)
        {
            var data = ListCartProducts.Where(x => x.IdOriginalProduct == orderProduct.IdOriginalProduct).FirstOrDefault();
            var index = ListCartProducts.IndexOf(data);
            ListCartProducts.RemoveAt(index);
            CalcAmount();
            RefresView();
        }

        public override Task InitializeAsync(object navigationData)
        {
            Store = SessionService.Get<Store>("KEY_SESSION_STORE");
            ListCartProducts = SessionService.Get<ObservableCollection<OrderProduct>>("ListCartProducts");
            PickupTime = DateTime.UtcNow.AddMinutes(15).TimeOfDay;
            CalcAmount();
            client = PreferencesService.GetUser();

            return base.InitializeAsync(navigationData);
        }

        private void CalcAmount()
        {
            double a = 0;
            foreach (var item in ListCartProducts)
            {
                a += item.UnitPrice;
            }

            Amount = a;
        }

        private async Task MakeOrderCommandAsync()
        {
            if (IsBusy)
            {
                return;
            }

            if (ListCartProducts.Count == 0)
            {
                return;
            }

            var order = new Order()
            {
                ClientId = client.Id,
                OrderProducts = ListCartProducts,
                Amount = this.Amount,
                PickupTime = DateTime.Today.ToUniversalTime().Add(this.PickupTime).ToUniversalTime(),
                StoreId = Store.Id,
                State = store.AutoAccept ? OrderStates.AUTOACCEPTED : OrderStates.STARTED,
            };

            var result = await TaskHelperFactory.
                                    CreateInternetAccessViewModelInstance(LoggingService, this).
                                    TryExecuteAsync(
                                    () => orderApi.Create(order));

            if (result)
            {
                SessionService.Save("ListCartProducts", new ObservableCollection<OrderProduct>());
                await NavigationService.NavigateToHome();
            }
        }

        public void RefresView()
        {
            CalcAmount();
            SessionService.Save("ListCartProducts", ListCartProducts);
        }
    }
}
