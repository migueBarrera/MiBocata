using Xamarin.Forms;

namespace MiBocata.Businnes.Framework
{
    public class BasePage : ContentPage
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
