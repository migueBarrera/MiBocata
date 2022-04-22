using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Businnes.iOS.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MiBocata.Businnes.iOS
{
    internal class PlatformDependencies : IDependencies
    {
        public void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IKeyboardService, KeyboardService>();
        }
    }
}