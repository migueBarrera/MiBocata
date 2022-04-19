using System;
using System.Threading.Tasks;
using MiBocata.Services.NotificationService;

namespace MiBocata.Services.Commons.NotificationService
{
    public class EmptyNotificationService : INotificationService
    {
        public EmptyNotificationService()
        {
        }

        public void Initialize()
        {
        }

        public Task SendPush(string idDevice, string title, string content)
        {
            return Task.CompletedTask;
        }
    }
}
