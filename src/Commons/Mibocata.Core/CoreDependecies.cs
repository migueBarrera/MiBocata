using Mibocata.Core.Framework;
using Mibocata.Core.Services;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Businnes.Services.Commons.AuthenticationService;
using Microsoft.Extensions.DependencyInjection;

namespace Mibocata.Core
{
    public class CoreDependecies : IDependencies
    {
        public void Register(IServiceCollection serviceCollection)
        {
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

#if DEBUG
            serviceCollection.AddTransient<ILoggingService, ReleaseLoggingService>();
#else
            serviceCollection.AddTransient<ILoggingService, ReleaseLoggingService>();
#endif
        }
    }
}
