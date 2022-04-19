using Autofac;
using MiBocata.Features.Cart;
using MiBocata.Features.Home;
using MiBocata.Features.LogIn;
using MiBocata.Features.Orders;
using MiBocata.Features.Profile;
using MiBocata.Features.Register;
using MiBocata.Features.Stores;
using MiBocata.Services.API.RefitServices;
using MiBocata.Services.AuthenticationService;
using MiBocata.Services.Commons.NotificationService;
using MiBocata.Services.ConnectivityServices;
using MiBocata.Services.DialogService;
using MiBocata.Services.GeolocationService;
using MiBocata.Services.LoggingService;
using MiBocata.Services.NavigationService;
using MiBocata.Services.NotificationService;
using MiBocata.Services.PreferencesService;
using MiBocata.Services.SessionService;
using System.Threading.Tasks;

namespace MiBocata.Framework
{
    public class Locator
    {
        private static readonly ContainerBuilder builder = new ContainerBuilder();

        public static IContainer Container { get; set; }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public static void Register<T>()
        {
            builder.RegisterType<T>();
        }

        public static void RegisterSingleton<T>()
        {
            builder.RegisterType<T>().SingleInstance();
        }

        public static void RegisterSingleton<TInterface, T>()
            where T : TInterface
        {
            builder.RegisterType<T>().As<TInterface>().SingleInstance();
        }

        public static Task RegisterDependencies()
        {
            // bool useMockServices = Settings.UseMocks;
            Register<HomeViewModel>();
            Register<ProfileViewModel>();
            Register<StoresViewModel>();
            Register<StoreDetailViewModel>();
            Register<CartViewModel>();
            Register<LogInControlViewModel>();
            Register<LogInViewModel>();
            Register<RegisterViewModel>();
            Register<EditProfileViewModel>();
            Register<OrdersViewModel>();
            Register<AddProductViewModel>();

            RegisterSingleton<IAuthenticationService, AuthenticationService>();
            RegisterSingleton<INavigationService, NavigationService>();
            RegisterSingleton<IMiBocataNavigationService, MiBocataNavigationService>();
            RegisterSingleton<IPreferencesService, PreferencesService>();
            RegisterSingleton<IGeolocationService, GeolocationService>();
            RegisterSingleton<INotificationService, EmptyNotificationService>();
            RegisterSingleton<ISessionService, SessionService>();
            //RegisterSingleton<INotificationService, OneSignalService>();
            RegisterSingleton<IDialogService, DialogService>();
            RegisterSingleton<IConnectivityService, ConnectivityService>();
            RegisterSingleton<IRefitService, RefitService>();

#pragma warning disable CS0162 // Se detectó código inaccesible
            if (DefaultSettings.DebugMode)
            {
                RegisterSingleton<ILoggingService, DebugLoggingService>();
            }
            else
            {
                RegisterSingleton<ILoggingService, AppCenterLoggingService>();
            }
#pragma warning restore CS0162 // Se detectó código inaccesible

            if (Container != null)
            {
                Container.Dispose();
            }

            Container = builder.Build();

            return Task.FromResult(true);
        }
    }
}
