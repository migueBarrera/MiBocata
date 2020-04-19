using System;
using System.Collections.Generic;
using System.Linq;
using Com.OneSignal;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace MiBocata.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            Xamarin.FormsGoogleMaps.Init("AIzaSyCM78zUBOKc8CxXY6peBStEmb0LusUd0G4");
            LoadApplication(new App());

            // TODO https://documentation.onesignal.com/docs/xamarin-sdk-setup 
            return base.FinishedLaunching(app, options);
        }
    }
}
