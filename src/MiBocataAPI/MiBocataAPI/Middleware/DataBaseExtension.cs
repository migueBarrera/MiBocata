namespace MiBocataAPI.Middleware;

public static class DataBaseExtension
{
    public static IServiceCollection AddDataBaseConfiguration(this IServiceCollection services, IConfiguration config)
    {
        var dataBaseName = config.GetSection("DataBase").GetSection("DataBaseName").Value;

        services.AddDbContext<MBDBContext>(
        dbContextOptions => dbContextOptions
            .UseInMemoryDatabase(dataBaseName)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
    );

        return services;
    }
}
