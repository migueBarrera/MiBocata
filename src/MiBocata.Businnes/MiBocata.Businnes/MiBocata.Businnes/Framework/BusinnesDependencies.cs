using MiBocata.Businnes.Features.Configuration;
using MiBocata.Businnes.Features.Home;
using MiBocata.Businnes.Features.LogIn;
using MiBocata.Businnes.Features.Orders;
using MiBocata.Businnes.Features.Products;
using MiBocata.Businnes.Features.Registro;
using MiBocata.Businnes.Features.Stores;
using MiBocata.Businnes.Services;
using MiBocata.Businnes.Services.API.RefitServices;
using MiBocata.Businnes.Services.Commons.DialogService;
using MiBocata.Businnes.Services.Commons.Navigation;
using MiBocata.Businnes.Services.Commons.NotificationService;
using MiBocata.Businnes.Services.Navigation;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MiBocata.Businnes.Framework
{
    internal class BusinnesDependencies : IDependencies
    {
        public void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IRefitService, RefitService>();
            serviceCollection.AddTransient<INavigationService, NavigationService>();
            serviceCollection.AddTransient<IMiBocataNavigationService, MiBocataNavigationService>();
            serviceCollection.AddTransient<IDialogService, DialogService>();
            serviceCollection.AddTransient<INotificationService, OneSignalService>();
            serviceCollection.AddTransient<IAppCenterSecretService, AppCenterSecretService>();
            serviceCollection.AddTransient<ILogInService, LogInService>();
            serviceCollection.AddTransient<IRegisterService, RegisterService>();
            serviceCollection.AddTransient<ICreateStoreService, CreateStoreService>();

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
