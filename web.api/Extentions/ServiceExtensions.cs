using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.API.DAL.Data;
using WEB.API.BLL;
using WEB.API.Contracts;
using WEB.API.Repositories;
using NLog;
using WEB.API.LoggerService;

namespace WEB.API.Extentions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
        public static void ConfigureSqlServerContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionString:JustInTimeDB"];
            services.AddDbContext<JustInTimeContext>(opts => opts.UseSqlServer(connectionString));
        }
        public static void ConfigureBLL(this IServiceCollection services)
        {
            services.AddScoped<EmployeeBLL>();
        }
        public static void ConfigureUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
