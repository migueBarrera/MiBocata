using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using Xamarin.Forms;

namespace MiBocata.Businnes.Services.Commons.TasksServices
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
            return new TaskHelper(dialogsService, connectivityService)
                .CheckInternetBeforeStarting(true)
                .WithLogging(logger);
        }

        public ITaskHelper CreateInternetAccessViewModelInstance(ILoggingService logger, CoreViewModel viewModel)
        {
            return CreateInternetAccessViewModelInstance(logger)
                .WhenStarting(() => Device.BeginInvokeOnMainThread(() => viewModel.IsBusy = true))
                .WhenFinished(() => Device.BeginInvokeOnMainThread(() => viewModel.IsBusy = false));
        }
    }
}