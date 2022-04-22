using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Businnes.Droid.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MiBocata.Businnes.Droid
{
    internal class PlarformDependencies : IDependencies
    {
        public void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IKeyboardService, KeyboardService>();
        }
    }
}