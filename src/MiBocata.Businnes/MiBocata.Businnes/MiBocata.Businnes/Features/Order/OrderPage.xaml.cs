using MiBocata.Businnes.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MiBocata.Businnes.Features.Order
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPage : BasePage
    {
        private bool isAlreadyInitialized;

        public OrderPage()
        {
            InitializeComponent();

            BindingContext = Locator.Resolve<OrderViewModel>();
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