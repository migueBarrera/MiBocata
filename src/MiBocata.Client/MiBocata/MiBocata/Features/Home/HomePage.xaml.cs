using Mibocata.Core.Framework;
using Xamarin.Forms;

namespace MiBocata.Features.Home
{
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ((CoreViewModel)BindingContext)?.InitializeAsync(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ((CoreViewModel)BindingContext)?.UnitializeAsync(null);
        }
    }
}