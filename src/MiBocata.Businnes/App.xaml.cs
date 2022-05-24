using MiBocata.Businnes.Features.Configuration;
using MiBocata.Businnes.Features.LogIn;
using MiBocata.Businnes.Features.Products;
using MiBocata.Businnes.Features.Registro;

namespace MiBocata.Businnes;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();

        Routing.RegisterRoute(nameof(LogInPage), typeof(LogInPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(ConfigurationPage), typeof(ConfigurationPage));
        Routing.RegisterRoute(nameof(ProductsPage), typeof(ProductsPage));
        Routing.RegisterRoute(nameof(NewProductPage), typeof(NewProductPage));
    }

    //protected override void OnStart()
    //{
    //    DependencyService.Resolve<ILoggingService>().Initialize();

    //    if (Device.RuntimePlatform != Device.UWP)
    //    {
    //        InitNavigation();
    //    }
    //}

    //protected override void OnSleep()
    //{
    //    // Handle when your app sleeps
    //}

    //protected override void OnResume()
    //{
    //    // Handle when your app resumes
    //}

    //private Task InitNavigation()
    //{
    //    var navigationService = this.DependencyService.Resolve<IMiBocataNavigationService>();
    //    return navigationService.InitializeAsync();
    //}

}