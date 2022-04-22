using Microsoft.Extensions.DependencyInjection;

namespace Mibocata.Core.Framework
{
    public interface IDependencies
    {
        void Register(IServiceCollection serviceCollection);
    }

}
