using Com.OneSignal;
using MiBocata.Framework;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MiBocata.Services.NotificationService
{
    public class OneSignalService : INotificationService
    {
        public void Initialize()
        {
            OneSignal
                .Current
                .StartInit("85a25b4a-1b52-41a6-ab91-11ff0a7e4834")
                .EndInit();

            OneSignal.Current.IdsAvailable(OneSignalCallback);

            OneSignal
                .Current
                .SetLogLevel(
                    Com.OneSignal.Abstractions.LOG_LEVEL.DEBUG,
                    Com.OneSignal.Abstractions.LOG_LEVEL.NONE);
        }

        public async Task SendPush(string idDevice, string title, string body)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri("https://onesignal.com/api/v1/notifications");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var obj = new
                {
                    app_id = DefaultSettings.OneSignalAppId,
                    contents = new { en = body },
                    headings = new { en = title },
                    include_player_ids = new string[] { idDevice },
                };
                var json = JsonConvert.SerializeObject(obj);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);
            }
        }

        private void OneSignalCallback(string userID, string pushToken)
        {
            Console.WriteLine("UserID:" + userID);
            Console.WriteLine("pushToken:" + pushToken);
        }
    }
}
