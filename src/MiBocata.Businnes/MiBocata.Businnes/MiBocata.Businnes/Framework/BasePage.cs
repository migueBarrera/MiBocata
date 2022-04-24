using Mibocata.Core.Framework;
using Xamarin.Forms;

namespace MiBocata.Businnes.Framework
{
    public class BasePage : ContentPage
    {
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
