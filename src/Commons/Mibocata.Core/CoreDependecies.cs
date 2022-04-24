using Mibocata.Core.Framework;
using Mibocata.Core.Services;
using Mibocata.Core.Services.Interfaces;
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

#if DEBUG
            serviceCollection.AddTransient<ILoggingService, DebugLoggingService>();
#else
            serviceCollection.AddTransient<ILoggingService, ReleaseLoggingService>();
#endif
        }
    }
}
