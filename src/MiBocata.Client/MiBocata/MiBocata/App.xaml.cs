using MiBocata.Services.NavigationService;

namespace MiBocata;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override void OnStart()
    {
        DependencyService.Resolve<ILoggingService>().Initialize();
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
}
