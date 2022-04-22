using System;
using System.Threading.Tasks;
using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Services.Commons.Navigation;
using MiBocata.Businnes.Services.Commons.NotificationService;
using Mibocata.Core.Extensions;
using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;

namespace MiBocata.Businnes
{
    public partial class App : Xamarin.Forms.Application
    {
        public App(IDependencies platformDependencies)
        {
            InitializeComponent();
            Current.On<Windows>().SetImageDirectory("Assets");
            this.DependencyService = ServicesCollection.GetServiceCollection(platformDependencies);
            if (Device.RuntimePlatform == Device.UWP)
            {
                InitNavigation();
            }

            InitOneSignal();
        }

        public static new App Current => Xamarin.Forms.Application.Current as App;

        public IServiceProvider DependencyService { get; private set; }

        protected override void OnStart()
        {
            DependencyService.Resolve<ILoggingService>().Initialize();

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
            var navigationService = this.DependencyService.Resolve<IMiBocataNavigationService>();
            return navigationService.InitializeAsync();
        }

        private void InitOneSignal()
        {
            var notificationService = DependencyService.Resolve<INotificationService>();
            notificationService.Initialize();
        }
    }
}