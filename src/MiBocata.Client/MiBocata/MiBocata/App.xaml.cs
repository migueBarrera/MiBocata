using MiBocata.Framework;
using MiBocata.Services.NavigationService;
using MiBocata.Services.NotificationService;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MiBocata
{
    public partial class App : Application
    {
        static App()
        {
            Locator.RegisterDependencies();
        }

        public App()
        {
            InitializeComponent();

            InitOneSignal();
        }

        protected override void OnStart()
        {
            InitNavigation();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private Task InitNavigation()
        {
            var navigationService = Locator.Resolve<IMiBocataNavigationService>();
            return navigationService.InitializeAsync();
        }

        private void InitOneSignal()
        {
            var notificationService = Locator.Resolve<INotificationService>();
            notificationService.Initialize();
        }
    }
}
