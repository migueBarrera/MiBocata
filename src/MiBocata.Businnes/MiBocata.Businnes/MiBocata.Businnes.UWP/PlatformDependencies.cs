using MiBocata.Businnes.UWP.Services;
using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MiBocata.Businnes.UWP
{
    internal class PlatformDependencies : IDependencies
    {
        public void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IKeyboardService, KeyboardService>();
        }
    }
}
