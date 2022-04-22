using Com.OneSignal;
using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Services.Commons.Preferences;
using System;
using Xamarin.Forms;

namespace MiBocata.Businnes.Services.Commons.NotificationService
{
    public class OneSignalService : INotificationService
    {
        private readonly IPreferencesService preferencesService;

        public OneSignalService(IPreferencesService preferencesService)
        {
            this.preferencesService = preferencesService;
        }

        public void Initialize()
        {
            if (Device.RuntimePlatform != Device.UWP)
            {
                OneSignal
                .Current
                .StartInit(DefaultSettings.OneSignalAppId)
                .EndInit();

                OneSignal.Current.IdsAvailable(OneSignalCallback);

                OneSignal
                    .Current
                    .SetLogLevel(
                        Com.OneSignal.Abstractions.LOG_LEVEL.DEBUG,
                        Com.OneSignal.Abstractions.LOG_LEVEL.NONE);
            }
        }

        private void OneSignalCallback(string userID, string pushToken)
        {
            preferencesService.SetPushToken(userID);

            Console.WriteLine("UserID:" + userID);
            Console.WriteLine("pushToken:" + pushToken);
        }
    }
}
