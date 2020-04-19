namespace MiBocata.Framework
{
    public static class DefaultSettings
    {
        public const string AppCenterAndroidSecret = "__ENTER_YOUR_ANDROID_APPCENTER_SECRET_HERE__";

        public const string AppCenteriOSSecret = "__ENTER_YOUR_IOS_APPCENTER_SECRET_HERE__";

        public const string AppCenterUWPSecret = "__ENTER_YOUR_UWP_APPCENTER_SECRET_HERE__";

        public const string OneSignalAppId = "__ENTER_YOUR_ONE_SIGNAL_ID_SECRET_HERE__";

        public const string URL_BASE = "__ENTER_YOUR_BASE_URL_HERE__";

        public const bool DebugMode =
#if DEBUG 
            true;
#else
            false;
#endif
    }
}
