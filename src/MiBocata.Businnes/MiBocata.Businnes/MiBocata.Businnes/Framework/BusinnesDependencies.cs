using MiBocata.Businnes.Features.Configuration;
using MiBocata.Businnes.Features.Home;
using MiBocata.Businnes.Features.LogIn;
using MiBocata.Businnes.Features.Orders;
using MiBocata.Businnes.Features.Products;
using MiBocata.Businnes.Features.Registro;
using MiBocata.Businnes.Features.Stores;
using MiBocata.Businnes.Services.API.RefitServices;
using MiBocata.Businnes.Services.Commons.AuthenticationService;
using MiBocata.Businnes.Services.Commons.DialogService;
using MiBocata.Businnes.Services.Commons.LoggingService;
using MiBocata.Businnes.Services.Commons.Navigation;
using MiBocata.Businnes.Services.Commons.NotificationService;
using MiBocata.Businnes.Services.Commons.Preferences;
using MiBocata.Businnes.Services.Commons.Products;
using MiBocata.Businnes.Services.Commons.Session;
using MiBocata.Businnes.Services.Commons.TasksServices;
using MiBocata.Businnes.Services.Navigation;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Framework;
using Mibocata.Core.Services;
using Mibocata.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MiBocata.Businnes.Framework
{
    internal class BusinnesDependencies : IDependencies
    {
        public void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ITaskHelper, TaskHelper>();
            serviceCollection.AddTransient<IRefitService, RefitService>();
            serviceCollection.AddTransient<ISessionService, SessionService>();
            serviceCollection.AddTransient<IAuthenticationService, AuthenticationService>();
            serviceCollection.AddTransient<INavigationService, NavigationService>();
            serviceCollection.AddTransient<IMiBocataNavigationService, MiBocataNavigationService>();
            serviceCollection.AddTransient<IPreferencesService, PreferencesService>();
            serviceCollection.AddTransient<IProductsService, ProductsService>();
            serviceCollection.AddTransient<IDialogService, DialogService>();
            serviceCollection.AddTransient<INotificationService, OneSignalService>();
            serviceCollection.AddTransient<ITaskHelperFactory, TaskHelperFactory>();

#pragma warning disable CS0162 // Se detectó código inaccesible
            if (DefaultSettings.DebugMode)
            {
                serviceCollection.AddTransient<ILoggingService, DebugLoggingService>();
            }
            else
            {
                serviceCollection.AddTransient<ILoggingService, AppCenterLoggingService>();
            }
#pragma warning restore CS0162 // Se detectó código inaccesible

            serviceCollection.AddTransient<LogInViewModel>();
            serviceCollection.AddTransient<RegisterViewModel>();
            serviceCollection.AddTransient<HomeViewModel>();
            serviceCollection.AddTransient<OrderViewModel>();
            serviceCollection.AddTransient<StoreViewModel>();
            serviceCollection.AddTransient<ConfigurationViewModel>();
            serviceCollection.AddTransient<ProductsViewModel>();
            serviceCollection.AddTransient<NewProductViewModel>();
            serviceCollection.AddTransient<CreateStoreViewModel>();
            serviceCollection.AddTransient<ChooseLocationViewModel>();
        }
    }
}
