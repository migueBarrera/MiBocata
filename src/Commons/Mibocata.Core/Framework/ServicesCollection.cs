using Mibocata.Core;
using Mibocata.Core.Framework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace MiBocata.Framework
{
    public class ServicesCollection
    {
        public static IServiceProvider GetServiceCollection(Assembly assembly, IDependencies platformDependencies)
        {
            var serviceCollection = new ServiceCollection();
            platformDependencies.Register(serviceCollection);
            new CoreDependecies().Register(serviceCollection);

            var dependenciesType = typeof(IDependencies);
            var allDependencies = assembly
                .GetTypes()
                .Where(type => !type.IsInterface)
                .Where(type => dependenciesType.IsAssignableFrom(type));

            foreach (Type type in allDependencies)
            {
                IDependencies dependency = (IDependencies)Activator.CreateInstance(type);
                dependency.Register(serviceCollection);
            }

            return serviceCollection.BuildServiceProvider();
        }
    }
}
