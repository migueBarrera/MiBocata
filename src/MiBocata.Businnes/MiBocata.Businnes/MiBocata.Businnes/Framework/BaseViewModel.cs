using System.Threading.Tasks;
using MiBocata.Businnes.Services.Commons.Navigation;
using MiBocata.Businnes.Services.Commons.Preferences;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using Xamarin.Forms;

namespace MiBocata.Businnes.Framework
{
    public class BaseViewModel : CoreViewModel
    {
        protected readonly INavigationService NavigationService;
        protected readonly IMiBocataNavigationService MiBocataNavigationService;
        protected readonly IPreferencesService PreferencesService;
        protected readonly ISessionService SessionService;
        protected readonly ILoggingService LoggingService;
        protected readonly IConnectivityService ConnectivityService;
        protected readonly ITaskHelper TaskHelper;
        protected readonly ITaskHelperFactory TaskHelperFactory;
        protected readonly IDialogService DialogService;
        protected readonly IRefitService RefitService;

        public BaseViewModel(
            INavigationService navigationService,
            IMiBocataNavigationService miBocataNavigationService,
            IPreferencesService preferencesService,
            ISessionService sessionService,
            ILoggingService loggingService,
            IDialogService dialogService,
            IConnectivityService connectivityService,
            ITaskHelper taskHelper,
            IRefitService refitService,
            ITaskHelperFactory taskHelperFactory)
        {
            TaskHelperFactory = taskHelperFactory;
            NavigationService = navigationService;
            MiBocataNavigationService = miBocataNavigationService;
            PreferencesService = preferencesService;
            SessionService = sessionService;
            LoggingService = loggingService;
            DialogService = dialogService;
            ConnectivityService = connectivityService;
            TaskHelper = taskHelper;
            RefitService = refitService;
        }
    }
}
