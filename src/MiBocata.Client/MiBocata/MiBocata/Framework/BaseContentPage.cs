using Mibocata.Core.Framework;
using Xamarin.Forms;

namespace MiBocata.Framework
{
    public class BaseContentPage : ContentPage
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
