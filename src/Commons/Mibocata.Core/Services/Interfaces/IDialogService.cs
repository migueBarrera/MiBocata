namespace Mibocata.Core.Services.Interfaces;

public interface IDialogService
{
    Task ShowMessage(string title, string content);

    Task ShowAlertAsync(string title, string content);
}
