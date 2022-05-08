namespace MiBocata.Services;

internal class AppCenterSecretService : IAppCenterSecretService
{
    public string GetSecret()
    {
        string appCenterSecret = string.Empty;

        //TODO
        //if (XFDevice.RuntimePlatform == XFDevice.Android)
        //{
        //    appCenterSecret = DefaultSettings.AppCenterAndroidSecret;
        //}
        //else if (XFDevice.RuntimePlatform == XFDevice.iOS)
        //{
        //    appCenterSecret = DefaultSettings.AppCenteriOSSecret;
        //}

        return appCenterSecret;
    }
}
