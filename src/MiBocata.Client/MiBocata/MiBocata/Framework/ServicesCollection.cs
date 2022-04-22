using Mibocata.Core.Framework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace MiBocata.Framework
{
    public class ServicesCollection
    {
        public static IServiceProvider GetServiceCollection(IDependencies platformDependencies)
        {
            var serviceCollection = new ServiceCollection();
            platformDependencies.Register(serviceCollection);
            new Mibocata.Core.CoreDependecies().Register(serviceCollection);

            var dependenciesType = typeof(IDependencies);
            var allDependencies = typeof(ServicesCollection)
                .Assembly
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
