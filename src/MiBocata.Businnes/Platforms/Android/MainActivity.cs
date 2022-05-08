using Android.App;
using Android.Content.PM;

namespace MiBocata;

[Activity(
  Theme = "@style/Maui.SplashTheme",
  MainLauncher = true,
  ConfigurationChanges =
  ConfigChanges.ScreenSize
  | ConfigChanges.Orientation
  | ConfigChanges.UiMode
  | ConfigChanges.ScreenLayout
  | ConfigChanges.SmallestScreenSize
  | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    //protected override void OnCreate(Bundle savedInstanceState)
    //{
    //    TabLayoutResource = Resource.Layout.Tabbar;
    //    ToolbarResource = Resource.Layout.Toolbar;

    //    base.OnCreate(savedInstanceState);

    //    Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

    //    CrossCurrentActivity.Current.Init(this, savedInstanceState);
    //    Xamarin.Essentials.Platform.Init(this, savedInstanceState);
    //    global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
    //    FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
    //    Xamarin.FormsMaps.Init(this, savedInstanceState);
    //    LoadApplication(new App(new PlarformDependencies()));
    //}

    //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
    //{
    //    Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

    //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
    //}

    //public override void OnBackPressed()
    //{
    //    if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
    //    {
    //        // Do something if there are some pages in the `PopupStack`
    //    }
    //    else
    //    {
    //        // Do something if there are not any pages in the `PopupStack`
    //    }
    //}
}