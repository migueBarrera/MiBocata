using Mibocata.Core.Features.Refit;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Framework;
using MiBocata.Services.NavigationService;
using MiBocata.Services.NotificationService;
using MiBocata.Services.PreferencesService;
using Models.Core;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiBocata.Features.Profile
{
    public class ProfileViewModel : BaseViewModel
    {
        private Client client;
        private bool userIsLogged;

        public ProfileViewModel(
            INotificationService notificationService,
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

        public bool UserIsLogged 
        { 
            get => userIsLogged; 
            set => SetAndRaisePropertyChanged(ref userIsLogged, value); 
        }

        public Client Client { get => client; set => SetAndRaisePropertyChanged(ref client, value); }

        public ICommand CloseSessionCommand => new AsyncCommand(_ => CloseSessionCommandAsync());

        public ICommand OrdersCommand => new AsyncCommand(_ => OrdersCommandAsync());

        public ICommand EditProfileCommand => new AsyncCommand(_ => EditProfileCommandAsync());

        public override Task InitializeAsync(object navigationData)
        {
            Client = PreferencesService.GetUser();

            UserIsLogged = Client != null;

            return base.InitializeAsync(navigationData);
        }

        private async Task CloseSessionCommandAsync()
        {
            PreferencesService.SetUser(null);
            await NavigationService.NavigateToHome();
        }

        private async Task OrdersCommandAsync()
        {
            await NavigationService.NavigateToOrders();
        }

        private async Task EditProfileCommandAsync()
        {
            await NavigationService.NavigateToEditProfile();
        }
    }
}
