using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Services.Commons.Navigation;
using MiBocata.Businnes.Services.Commons.Preferences;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Services.Interfaces;

namespace MiBocata.Businnes.Features.Home
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel(
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
            : base(
                  navigationService,
                  miBocataNavigationService,
                  preferencesService,
                  sessionService,
                  loggingService,
                  dialogService,
                  connectivityService,
                  taskHelper,
                  refitService,
                  taskHelperFactory)
        {
        }
    }
}
