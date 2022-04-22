using Mibocata.Core.Services.Interfaces;
using MiBocata.Framework;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using XFDevice = Xamarin.Forms.Device;

namespace MiBocata.Services.LoggingService
{
    public class AppCenterLoggingService : ILoggingService
    {
        public AppCenterLoggingService()
        {
        }

        public void Initialize()
        {
            string appCenterSecret = string.Empty;

            if (XFDevice.RuntimePlatform == XFDevice.Android)
            {
                appCenterSecret = DefaultSettings.AppCenterAndroidSecret;
            }
            else if (XFDevice.RuntimePlatform == XFDevice.iOS)
            {
                appCenterSecret = DefaultSettings.AppCenteriOSSecret;
            }

            AppCenter.LogLevel = LogLevel.Verbose;
            AppCenter.Start(appCenterSecret, typeof(Analytics), typeof(Crashes));
        }

        public void Debug(string message)
        {
            Analytics.TrackEvent(nameof(Debug), new Dictionary<string, string> { { nameof(message), message } });
        }

        public void Warning(string message)
        {
            Analytics.TrackEvent(nameof(Warning), new Dictionary<string, string> { { nameof(message), message } });
        }

        public void Error(Exception exception)
        {
            Crashes.TrackError(exception);
        }
    }
}
