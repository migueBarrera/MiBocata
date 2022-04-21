namespace MiBocataAPI.Middleware;

public static class DataBaseExtension
{
    public static IServiceCollection AddDataBaseConfiguration(this IServiceCollection services, IConfiguration config)
    {

        var userDB = config.GetSection("DataBase").GetSection("User").Value;
        var passDB = config.GetSection("DataBase").GetSection("Pass").Value;
        var dataBaseName = config.GetSection("DataBase").GetSection("DataBaseName").Value;

        var connectionString = $"server=localhost;user={userDB};password={passDB};database={dataBaseName}";

        var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));

        services.AddDbContext<MBDBContext>(
        dbContextOptions => dbContextOptions
            .UseMySql(connectionString, serverVersion)
            // The following three options help with debugging, but should
            // be changed or removed for production.
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
    );

        return services;
    }
}
