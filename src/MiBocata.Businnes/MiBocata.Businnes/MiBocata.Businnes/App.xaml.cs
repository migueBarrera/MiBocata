using MiBocata.Businnes.Features.LogIn;
using MiBocata.Businnes.Features.Registro;
using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Services.LoggingService;
using MiBocata.Businnes.Services.Navigation;
using MiBocata.Businnes.Services.NotificationService;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
using Xamarin.Forms.Xaml;

namespace MiBocata.Businnes
{
    public partial class App : Xamarin.Forms.Application
    {
        static App()
        {
            Locator.RegisterDependencies();
        }

        public App()
        {
            InitializeComponent();
            Current.On<Windows>().SetImageDirectory("Assets");
            if (Device.RuntimePlatform == Device.UWP)
            {
                InitNavigation();
            }

            InitOneSignal();
        }

        protected override void OnStart()
        {
            Locator.Resolve<ILoggingService>().Initialize();

            if (Device.RuntimePlatform != Device.UWP)
            {
                InitNavigation();
            }
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