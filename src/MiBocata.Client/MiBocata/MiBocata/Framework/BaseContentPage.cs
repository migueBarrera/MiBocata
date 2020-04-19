using Xamarin.Forms;

namespace MiBocata.Framework
{
    public class BaseContentPage : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();

            ((BaseViewModel)BindingContext)?.InitializeAsync(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ((BaseViewModel)BindingContext)?.UnitializeAsync(null);
        }
    }
}
