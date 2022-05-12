using MiBocata.Features.Cart;
using MiBocata.Features.LogIn;
using MiBocata.Features.Orders;
using MiBocata.Features.Profile;
using MiBocata.Features.Register;
using MiBocata.Features.Stores;
using MiBocata.Services.DialogService;

namespace MiBocata.Framework;

internal static class ServiceExtension
{
    internal static MauiAppBuilder ConfigurePages(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<LogInPage>();
        builder.Services.AddSingleton<RegisterPage>();
        builder.Services.AddSingleton<StoresPage>();
        builder.Services.AddSingleton<ProfilePage>();
        builder.Services.AddTransient<EditProfilePage>();
        builder.Services.AddTransient<OrdersPage>();
        builder.Services.AddTransient<StoreDetailPage>();
        builder.Services.AddTransient<AddProductPage>();

        return builder;
    }
    
    internal static MauiAppBuilder ConfigureViewModels(this MauiAppBuilder builder)
    {
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

        return builder;
    }
    
    internal static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<IAppCenterSecretService, AppCenterSecretService>();
        builder.Services.AddTransient<IAppSecretsService, AppSecretsService>();
        builder.Services.AddTransient<IDialogService, DialogService>();
        builder.Services.AddTransient<IRegisterService, RegisterService>();

#if ANDROID
        builder.Services.AddTransient<IKeyboardService, Client.Platforms.Android.Services.KeyboardService>();
#endif
        return builder;
    }
}
