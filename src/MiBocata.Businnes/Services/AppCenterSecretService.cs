using MiBocata.Businnes.Framework;
using Mibocata.Core.Services.Interfaces;

namespace MiBocata.Businnes.Services
{
    internal class AppCenterSecretService : IAppCenterSecretService
    {
        public string GetSecret()
        {
            string appCenterSecret = string.Empty;

            //if (XFDevice.RuntimePlatform == XFDevice.Android)
            //{
            //    appCenterSecret = DefaultSettings.AppCenterAndroidSecret;
            //}
            //else if (XFDevice.RuntimePlatform == XFDevice.iOS)
            //{
            //    appCenterSecret = DefaultSettings.AppCenteriOSSecret;
            //}
            //else
            //{
            //    appCenterSecret = DefaultSettings.AppCenterUWPSecret;
            //}

            return appCenterSecret;
        }
    }
}
