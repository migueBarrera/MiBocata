using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Services.ConnectivityServices;
using MiBocata.Businnes.Services.DialogService;
using MiBocata.Businnes.Services.LoggingService;
using Xamarin.Forms;

namespace MiBocata.Businnes.Services.TasksServices
{
    public class TaskHelperFactory
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
            return new TaskHelper(this.dialogsService, this.connectivityService)
                .CheckInternetBeforeStarting(true)
                .WithLogging(logger);
        }

        public ITaskHelper CreateInternetAccessViewModelInstance(ILoggingService logger, BaseViewModel viewModel)
        {
            return this.CreateInternetAccessViewModelInstance(logger)
                .WhenStarting(() => Device.BeginInvokeOnMainThread(() => viewModel.IsBusy = true))
                .WhenFinished(() => Device.BeginInvokeOnMainThread(() => viewModel.IsBusy = false));
        }
    }
}