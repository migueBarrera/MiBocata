using Android.Content;
using Android.Views.InputMethods;
using Mibocata.Core.Services.Interfaces;
using Plugin.CurrentActivity;

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