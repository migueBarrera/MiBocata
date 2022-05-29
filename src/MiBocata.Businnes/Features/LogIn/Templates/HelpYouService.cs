using Mibocata.Core.Services.Interfaces;

namespace Mibocata.Businnes.Features.LogIn.Templates;

public class HelpYouService : IHelpYouService
{
    private readonly IDialogService dialogService;
    private readonly ILoggingService loggingService;

    public HelpYouService(
        IDialogService dialogService, 
        ILoggingService loggingService)
    {
        this.dialogService = dialogService;
        this.loggingService = loggingService;
    }

    public async Task CallUsAsync()
    {
        loggingService.Debug($"{nameof(CallUsAsync)} called");
        if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            await dialogService.ShowAlertAsync("603033613", "Llamenos sin compromiso");
            return;
        }

        try
        {
            PhoneDialer.Open("603033613");
        }
        catch (Exception e)
        {
            loggingService.Error(e);
            await dialogService.ShowAlertAsync("603033613", "Llamenos sin compromiso");
        }
    }
}
