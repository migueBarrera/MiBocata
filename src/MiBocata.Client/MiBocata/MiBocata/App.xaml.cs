using Mibocata.Core.Extensions;
using Mibocata.Core.Framework;
using MiBocata.Framework;
using MiBocata.Services.NavigationService;
using MiBocata.Services.NotificationService;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MiBocata
{
    public partial class App : Application
    {
        public App(IDependencies platformDependencies)
        {
            InitializeComponent();
            this.DependencyService = ServicesCollection.GetServiceCollection(platformDependencies);
            //InitOneSignal();
        }

        public static new App Current => Xamarin.Forms.Application.Current as App;

        public IServiceProvider DependencyService { get; private set; }

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
            var navigationService = DependencyService.Resolve<IMiBocataNavigationService>();
            return navigationService.InitializeAsync();
        }

        private void InitOneSignal()
        {
            var notificationService = DependencyService.Resolve<INotificationService>();
            notificationService.Initialize();
        }
    }
}
