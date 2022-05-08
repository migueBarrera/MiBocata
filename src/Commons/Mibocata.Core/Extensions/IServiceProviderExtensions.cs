namespace Mibocata.Core.Extensions;

public static class IServiceProviderExtensions
{
    public static T Resolve<T>(this IServiceProvider provider) => provider.Resolve<T>(typeof(T));

    public static T Resolve<T>(this IServiceProvider provider, Type type)
    {
        var resolved = provider.GetService(type);

        if (resolved == null)
        {
            throw new ArgumentException($"You forgot to register {typeof(T)} in DI container!");
        }

        return (T)resolved;
    }
}
