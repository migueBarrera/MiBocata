using Autofac;
using MiBocata.Businnes.Features.Configuration;
using MiBocata.Businnes.Features.Home;
using MiBocata.Businnes.Features.LogIn;
using MiBocata.Businnes.Features.Order;
using MiBocata.Businnes.Features.Products;
using MiBocata.Businnes.Features.Registro;
using MiBocata.Businnes.Features.Store;
using MiBocata.Businnes.Services.API.RefitServices;
using MiBocata.Businnes.Services.AuthenticationService;
using MiBocata.Businnes.Services.ConnectivityServices;
using MiBocata.Businnes.Services.DialogService;
using MiBocata.Businnes.Services.GeocodingService;
using MiBocata.Businnes.Services.GeolocationService;
using MiBocata.Businnes.Services.ImagesService;
using MiBocata.Businnes.Services.LoggingService;
using MiBocata.Businnes.Services.Navigation;
using MiBocata.Businnes.Services.NotificationService;
using MiBocata.Businnes.Services.Preferences;
using MiBocata.Businnes.Services.Products;
using MiBocata.Businnes.Services.Session;
using MiBocata.Businnes.Services.TasksServices;
using System.Threading.Tasks;

namespace MiBocata.Businnes.Framework
{
    public static class Locator
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

        /// <summary>
        ///  Se registran las dependencias en Autofac 
        /// </summary>
        public static Task RegisterDependencies()
        {
            var useFakeServices = false;

            // bool useMockServices = Settings.UseMocks;
            RegisterSingleton<LogInViewModel>();
            RegisterSingleton<RegisterViewModel>();
            RegisterSingleton<HomeViewModel>();
            RegisterSingleton<OrderViewModel>();
            RegisterSingleton<StoreViewModel>();
            RegisterSingleton<ConfigurationViewModel>();
            RegisterSingleton<ProductsViewModel>();
            RegisterSingleton<NewProductViewModel>();
            RegisterSingleton<CreateStoreViewModel>();
            RegisterSingleton<ChooseLocationViewModel>();

            RegisterSingleton<ISessionService, SessionService>();
            RegisterSingleton<IAuthenticationService, AuthenticationService>();
            RegisterSingleton<INavigationService, NavigationService>();
            RegisterSingleton<IMiBocataNavigationService, MiBocataNavigationService>();
            RegisterSingleton<IPreferencesService, PreferencesService>();
            RegisterSingleton<IProductsService, ProductsService>(); 
            RegisterSingleton<IDialogService, DialogService>();
            RegisterSingleton<IGeocodingService, GeocodingService>();
            RegisterSingleton<IGeolocationService, GeolocationService>();
            RegisterSingleton<INotificationService, OneSignalService>();
            RegisterSingleton<IImagesService, ImagesService>();
            RegisterSingleton<IConnectivityService, ConnectivityService>();
            RegisterSingleton<ITaskHelper, TaskHelper>();
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

            if (useFakeServices)
            {
                ////RegisterSingleton<IOrderService, FakeOrderService>();

                //// RegisterSingleton<IProductsService, FakeProductsService>();
            }

            if (Container != null)
            {
                Container.Dispose();
            }

            Container = builder.Build();

            return Task.FromResult(true);
        }
    }
}