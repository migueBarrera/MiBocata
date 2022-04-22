using Mibocata.Core.Features.Refit;
using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Services.NavigationService;
using MiBocata.Services.PreferencesService;
using PubSub;
using Xamarin.Forms;

namespace MiBocata.Framework
{
    public class BaseViewModel : CoreViewModel
    {
        protected readonly IMiBocataNavigationService NavigationService;
        protected readonly IPreferencesService PreferencesService;
        protected readonly ISessionService SessionService;
        protected readonly IKeyboardService KeyboardService;
        protected readonly ILoggingService LoggingService;
        protected readonly IDialogService DialogService;
        protected readonly IConnectivityService ConnectivityService;
        protected readonly IRefitService RefitService;
        protected Hub Hub = Hub.Default;

        protected readonly ITaskHelperFactory TaskHelperFactory;

        public BaseViewModel(
            IMiBocataNavigationService navigationService,
            IPreferencesService preferencesService,
            ISessionService sessionService,
            ILoggingService loggingService,
            IDialogService dialogService,
            IConnectivityService connectivityService,
            IRefitService refitService,
            ITaskHelperFactory taskHelperFactory,
            IKeyboardService keyboardService)
        {
            KeyboardService = keyboardService;
            TaskHelperFactory = taskHelperFactory;
            NavigationService = navigationService;
            PreferencesService = preferencesService;
            SessionService = sessionService;
            LoggingService = loggingService;
            DialogService = dialogService;
            ConnectivityService = connectivityService;
            RefitService = refitService;
        }
    }
}
