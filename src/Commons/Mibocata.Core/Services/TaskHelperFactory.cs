using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;

namespace Mibocata.Core.Services;

public class TaskHelperFactory : ITaskHelperFactory
{
    private readonly IDialogService dialogsService;
    private readonly IConnectivityService connectivityService;

    public TaskHelperFactory(IDialogService dialogsService, IConnectivityService connectivityService)
    {
        this.dialogsService = dialogsService;
        this.connectivityService = connectivityService;
    }

    public ITaskHelper CreateInternetAccessViewModelInstance(ILoggingService logger)
    {
        return new TaskHelper(dialogsService, connectivityService) //// TODO use DI?
            .CheckInternetBeforeStarting(true)
            .WithLogging(logger);
    }

    public ITaskHelper CreateInternetAccessViewModelInstance(ILoggingService logger, IBusyViewModel busyViewModel)
    {
        return CreateInternetAccessViewModelInstance(logger)
            .WhenStarting(() => MainThread.BeginInvokeOnMainThread(() => busyViewModel.IsBusy = true))
            .WhenFinished(() => MainThread.BeginInvokeOnMainThread(() => busyViewModel.IsBusy = false));
    }
}