using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using Xamarin.Forms;

namespace MiBocata.Services.TasksServices
{
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
            return new TaskHelper(this.dialogsService, this.connectivityService)
                .CheckInternetBeforeStarting(true)
                .WithLogging(logger);
        }

        public ITaskHelper CreateInternetAccessViewModelInstance(ILoggingService logger, CoreViewModel viewModel)
        {
            return this.CreateInternetAccessViewModelInstance(logger)
                .WhenStarting(() => Device.BeginInvokeOnMainThread(() => viewModel.IsBusy = true))
                .WhenFinished(() => Device.BeginInvokeOnMainThread(() => viewModel.IsBusy = false));
        }
    }
}