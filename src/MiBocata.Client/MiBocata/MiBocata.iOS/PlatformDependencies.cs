using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using MiBocata.iOS.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MiBocata.iOS
{
    public class PlatformDependencies : IDependencies
    {
        public void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IKeyboardService, KeyboardService>();
        }
    }
}