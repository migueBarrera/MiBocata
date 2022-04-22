using System.Threading.Tasks;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Framework;
using MiBocata.Services.NavigationService;
using MiBocata.Services.PreferencesService;

namespace MiBocata.Features.LogIn
{
    public class LogInViewModel : BaseViewModel
    {
        public LogInViewModel(
            IMiBocataNavigationService navigationService,
            IPreferencesService preferencesService,
            ISessionService sessionService,
            ILoggingService loggingService,
            IDialogService dialogService,
            IConnectivityService connectivityService,
            IRefitService refitService,
            ITaskHelperFactory taskHelperFactory,
            IKeyboardService keyboardService)
            : base(
                  navigationService,
                  preferencesService,
                  sessionService,
                  loggingService,
                  dialogService,
                  connectivityService,
                  refitService,
                  taskHelperFactory,
                  keyboardService)
        {
        }

        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }
    }
}
