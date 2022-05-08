using Mibocata.Core.Services.Interfaces;

namespace MiBocata.Businnes.Services.Commons.DialogService
{
    public class DialogService : IDialogService
    {
        public Task ShowAlertAsync(string title, string content)
        {
            return Application.Current.MainPage.DisplayAlert(
                title,
                content,
                "OK");
        }

        [Obsolete("This method has been deprecated. Use ShowAlertAsync instead.")]
        public async Task ShowMessage(
            string message,
            string title)
        {
            await Application.Current.MainPage.DisplayAlert(
                title,
                message,
                "OK");
        }
    }
}
