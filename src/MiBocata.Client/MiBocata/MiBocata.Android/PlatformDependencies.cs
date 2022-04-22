using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Droid.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MiBocata.Droid
{
    internal class PlatformDependencies : IDependencies
    {
        public void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IKeyboardService, KeyboardService>();
        }
    }
}