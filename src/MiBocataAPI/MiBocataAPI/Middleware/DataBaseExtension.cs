using MiBocataAPI.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace MiBocataAPI.Middleware
{
    public static class DataBaseExtension
    {
        public static IServiceCollection AddDataBaseConfiguration(this IServiceCollection services, IConfiguration config)
        {
            var userDB = config.GetSection("DataBase").GetSection("User").Value;
            var passDB = config.GetSection("DataBase").GetSection("Pass").Value;
            var dataBaseName = config.GetSection("DataBase").GetSection("DataBaseName").Value;

            services
                .AddDbContextPool<MBDBContext>(options => options
                .UseMySql($"Server=localhost;Database={dataBaseName};User={userDB};Password={passDB};", mySqlOptions => mySqlOptions
                    .ServerVersion(new Version(10, 4, 11), ServerType.MariaDb)
            ));

            return services;
        }
    }
}
