using Mibocata.Core.Features.Refit;
using Mibocata.Core.Framework;
using Mibocata.Core.Services;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Features.Cart;
using MiBocata.Features.Home;
using MiBocata.Features.LogIn;
using MiBocata.Features.Orders;
using MiBocata.Features.Profile;
using MiBocata.Features.Register;
using MiBocata.Features.Stores;
using MiBocata.Services;
using MiBocata.Services.API.RefitServices;
using MiBocata.Services.AuthenticationService;
using MiBocata.Services.Commons.NotificationService;
using MiBocata.Services.DialogService;
using MiBocata.Services.NavigationService;
using MiBocata.Services.NotificationService;
using MiBocata.Services.PreferencesService;
using MiBocata.Services.SessionService;
using MiBocata.Services.TasksServices;
using Microsoft.Extensions.DependencyInjection;

namespace MiBocata.Framework
{
    internal class ClientDependencies : IDependencies
    {
        public void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<HomeViewModel>();
            serviceCollection.AddTransient<ProfileViewModel>();
            serviceCollection.AddTransient<StoresViewModel>();
            serviceCollection.AddTransient<StoreDetailViewModel>();
            serviceCollection.AddTransient<CartViewModel>();
            serviceCollection.AddTransient<LogInControlViewModel>();
            serviceCollection.AddTransient<LogInViewModel>();
            serviceCollection.AddTransient<RegisterViewModel>();
            serviceCollection.AddTransient<EditProfileViewModel>();
            serviceCollection.AddTransient<OrdersViewModel>();
            serviceCollection.AddTransient<AddProductViewModel>();

            serviceCollection.AddTransient<IAppCenterSecretService, AppCenterSecretService>();
            serviceCollection.AddTransient<IAuthenticationService, AuthenticationService>();
            serviceCollection.AddTransient<INavigationService, NavigationService>();
            serviceCollection.AddTransient<IMiBocataNavigationService, MiBocataNavigationService>();
            serviceCollection.AddTransient<IPreferencesService, PreferencesService>();
            serviceCollection.AddTransient<INotificationService, EmptyNotificationService>();
            serviceCollection.AddTransient<ISessionService, SessionService>();
            serviceCollection.AddTransient<IDialogService, DialogService>();
            serviceCollection.AddTransient<IRefitService, RefitService>();
            serviceCollection.AddTransient<ITaskHelperFactory, TaskHelperFactory>();
        }
    }
}
