namespace MiBocata.Framework;

public static class DefaultSettings
{
    public const string AppCenterAndroidSecret = "8e701712-894c-42f0-9364-8b9cc5f59aea";

    public const string AppCenteriOSSecret = "b0e29123-0a7d-4041-bf28-9c6f5438c7ab";

    public const string OneSignalAppId = "__ENTER_YOUR_ONE_SIGNAL_ID_SECRET_HERE__";

    public const string URL_BASE = "http://141.94.207.18";

    public const bool DebugMode =
#if DEBUG 
        true;
#else
        false;
#endif
}
