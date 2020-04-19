using MiBocata.Businnes.Framework;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;

namespace MiBocata.Businnes.Services.LoggingService
{
    public class AppCenterLoggingService : ILoggingService
    {
        public AppCenterLoggingService()
        {
        }

        public void Initialize()
        {
            AppCenter.LogLevel = LogLevel.Verbose;
            AppCenter.Start(
                $"ios={DefaultSettings.AppCenteriOSSecret};" +
                $"​android={DefaultSettings.AppCenterAndroidSecret};" +
                $"uwp={DefaultSettings.AppCenterUWPSecret}",
                typeof(Analytics),
                typeof(Crashes));
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
