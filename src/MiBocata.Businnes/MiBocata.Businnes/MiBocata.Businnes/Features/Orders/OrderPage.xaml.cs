using MiBocata.Businnes.Framework;
using Mibocata.Core.Extensions;

namespace MiBocata.Businnes.Features.Orders
{
    public partial class OrderPage : BasePage
    {
        private bool isAlreadyInitialized;

        public OrderPage()
        {
            InitializeComponent();

            BindingContext = App.Current.DependencyService.Resolve<OrderViewModel>();
        }

        protected override void OnAppearing()
        {
            if (!isAlreadyInitialized)
            {
                base.OnAppearing();
                isAlreadyInitialized = true;
            }
        }
    }
}