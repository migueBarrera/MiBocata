namespace MiBocata.Businnes.Framework
{
    public static class DefaultSettings
    {
        public const string AppCenterAndroidSecret = "f77f564b-c7dc-41f3-823e-1e3040ccdcf1";

        public const string AppCenteriOSSecret = "72377cf8-caa4-4b0a-a932-e4c45923fb12";

        public const string AppCenterUWPSecret = "18fb7c27-4cf4-4851-af4d-da6a05a752e6"; 

        public const string OneSignalAppId = "__ENTER_YOUR_ONE_SIGNAL_ID_SECRET_HERE__";

        public const string URL_BASE = "http://141.94.207.18";

        public const bool DebugMode =
#if DEBUG 
            true;
#else
            false;
#endif
    }
}
