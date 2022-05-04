using MiBocata.Features.Cart;
using MiBocata.Features.Home;
using MiBocata.Features.LogIn;
using MiBocata.Features.Orders;
using MiBocata.Features.Profile;
using MiBocata.Features.Register;
using MiBocata.Features.Stores;
using MiBocata.Services.Commons.NotificationService;
using MiBocata.Services.DialogService;
using MiBocata.Services.NavigationService;
using MiBocata.Services.NotificationService;

namespace MiBocata.Framework;

internal static class ServiceExtension
{
    internal static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<ProfileViewModel>();
        builder.Services.AddTransient<StoresViewModel>();
        builder.Services.AddTransient<StoreDetailViewModel>();
        builder.Services.AddTransient<CartViewModel>();
        builder.Services.AddTransient<LogInControlViewModel>();
        builder.Services.AddTransient<LogInViewModel>();
        builder.Services.AddTransient<RegisterViewModel>();
        builder.Services.AddTransient<EditProfileViewModel>();
        builder.Services.AddTransient<OrdersViewModel>();
        builder.Services.AddTransient<AddProductViewModel>();


        builder.Services.AddTransient<IAppCenterSecretService, AppCenterSecretService>();
        builder.Services.AddTransient<IAppSecretsService, AppSecretsService>();
        builder.Services.AddTransient<INavigationService, NavigationService>();
        builder.Services.AddTransient<IMiBocataNavigationService, MiBocataNavigationService>();
        builder.Services.AddTransient<INotificationService, EmptyNotificationService>();
        builder.Services.AddTransient<IDialogService, DialogService>();
        builder.Services.AddTransient<IRegisterService, RegisterService>();

#if ANDROID
        builder.Services.AddTransient<IKeyboardService, Client.Platforms.Android.Services.KeyboardService>();
#endif
        return builder;
    }
}
