namespace MiBocata.Client.Platforms.Android.Services
{
    public class KeyboardService : IKeyboardService
    {
        public void HideSoftKeyboard()
        {
            //var currentFocus = CrossCurrentActivity.Current.Activity.CurrentFocus;
            //if (currentFocus != null)
            //{
            //    InputMethodManager inputMethodManager = (InputMethodManager)CrossCurrentActivity.Current.Activity.GetSystemService(Context.InputMethodService);
            //    inputMethodManager.HideSoftInputFromWindow(currentFocus.WindowToken, HideSoftInputFlags.None);
            //}
        }
    }
}