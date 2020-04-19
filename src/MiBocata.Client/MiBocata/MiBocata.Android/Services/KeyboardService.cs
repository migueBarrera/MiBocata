using Android.Content;
using Android.Views.InputMethods;
using MiBocata.Droid.Services;
using MiBocata.Services.KeyboardService;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(KeyboardService))]

namespace MiBocata.Droid.Services
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