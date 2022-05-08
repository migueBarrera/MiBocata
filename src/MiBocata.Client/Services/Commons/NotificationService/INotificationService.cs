namespace MiBocata.Services.NotificationService
{
    public interface INotificationService
    {
        void Initialize();

        Task SendPush(string idDevice, string title, string content);
    }
}
