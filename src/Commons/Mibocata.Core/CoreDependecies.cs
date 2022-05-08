using Mibocata.Core.Features.Refit;
using Mibocata.Core.Services;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Businnes.Services.Commons.AuthenticationService;

namespace Mibocata.Core;

public static class CoreDependecies
{
    public static MauiAppBuilder ConfigureCoreServices(this MauiAppBuilder builder)
    {
        var serviceCollection = builder.Services;

        serviceCollection.AddTransient<IConnectivityService, ConnectivityService>();
        serviceCollection.AddTransient<IGeolocationService, GeolocationService>();
        serviceCollection.AddTransient<IGeocodingService, GeocodingService>();
        serviceCollection.AddTransient<ILogFileSystemService, LogFileSystemService>();
        serviceCollection.AddTransient<IAppCenterService, AppCenterService>();
        serviceCollection.AddTransient<IFileLoggingService, FileLoggingService>();
        serviceCollection.AddTransient<ITaskHelper, TaskHelper>();
        serviceCollection.AddTransient<ITaskHelperFactory, TaskHelperFactory>();
        serviceCollection.AddTransient<IAuthenticationService, AuthenticationService>();
        serviceCollection.AddSingleton<ISessionService, SessionService>();
        serviceCollection.AddTransient<IPreferencesService, PreferencesService>();
        serviceCollection.AddTransient<IRefitService, RefitService>();

#if DEBUG
        serviceCollection.AddTransient<ILoggingService, ReleaseLoggingService>();
#else
        serviceCollection.AddTransient<ILoggingService, ReleaseLoggingService>();
#endif

        return builder;
    }
}
