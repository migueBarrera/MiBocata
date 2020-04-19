using System.Threading.Tasks;

namespace MiBocata.Services.DialogService
{
    public interface IDialogService
    {
        Task ShowMessage(string title, string content);

        Task ShowAlertAsync(string title, string content);
    }
}
