using Mibocata.Businnes.Features.LogIn.Templates;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Businnes.Features.Configuration;
using MiBocata.Businnes.Features.LogIn;
using MiBocata.Businnes.Features.Orders;
using MiBocata.Businnes.Features.Products;
using MiBocata.Businnes.Features.Registro;
using MiBocata.Businnes.Features.Stores;
using MiBocata.Businnes.Services;
using MiBocata.Businnes.Services.Commons.DialogService;
using MiBocata.Features.LogIn;

namespace MiBocata.Framework;

internal static class ServiceExtension
{
    internal static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<IAppSecretsService, AppSecretsService>();
        builder.Services.AddTransient<IDialogService, DialogService>();
        builder.Services.AddTransient<IAppCenterSecretService, AppCenterSecretService>();
        builder.Services.AddTransient<ILogInService, LogInService>();
        builder.Services.AddTransient<IRegisterService, RegisterService>();
        builder.Services.AddTransient<ICreateStoreService, CreateStoreService>();
        builder.Services.AddTransient<IChooseLocationService, ChooseLocationService>();
        builder.Services.AddTransient<IHelpYouService, HelpYouService>();

#if ANDROID
        builder.Services.AddTransient<IKeyboardService, MiBocata.Platforms.Android.Services.KeyboardService>();
#elif WINDOWS
        builder.Services.AddTransient<IKeyboardService, MiBocata.Businnes.WinUI.Services.KeyboardService>();
#endif
        return builder;
    }

    internal static MauiAppBuilder ConfigureViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<LogInViewModel>();
        builder.Services.AddTransient<RegisterViewModel>();
        builder.Services.AddTransient<OrderViewModel>();
        builder.Services.AddTransient<StoreViewModel>();
        builder.Services.AddTransient<ConfigurationViewModel>();
        builder.Services.AddTransient<ProductsViewModel>();
        builder.Services.AddTransient<NewProductViewModel>();
        builder.Services.AddTransient<CreateStoreViewModel>();
        builder.Services.AddTransient<ChooseLocationViewModel>();

        return builder;
    }

    internal static MauiAppBuilder ConfigurePages(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<LogInPage>();
        builder.Services.AddTransient<RegisterPage>();
        builder.Services.AddSingleton<OrderPage>();
        builder.Services.AddSingleton<StorePage>();
        builder.Services.AddTransient<ConfigurationPage>();
        builder.Services.AddTransient<ProductsPage>();
        builder.Services.AddTransient<NewProductPage>();
        builder.Services.AddTransient<CreateStorePage>();
        builder.Services.AddTransient<ChooseLocationPage>();

        return builder;
    }
}

