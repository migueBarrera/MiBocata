using Android.Content;
using Android.Views.InputMethods;
using MiBocata.Businnes.Droid.Services;
using MiBocata.Businnes.Services.KeyboardService;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(KeyboardService))]

namespace MiBocata.Businnes.Droid.Services
{
    public class KeyboardService : IKeyboardService
    {
        public void HideSoftKeyboard()
        {
            var currentFocus = CrossCurrentActivity.Current.Activity.CurrentFocus;
            if (currentFocus != null)
            {
                InputMethodManager inputMethodManager = (InputMethodManager)CrossCurrentActivity.Current.Activity.GetSystemService(Context.InputMethodService);
                inputMethodManager.HideSoftInputFromWindow(currentFocus.WindowToken, HideSoftInputFlags.None);
            }
        }
    }
}