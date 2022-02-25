using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SellerCRUD.Domain.Interfaces;
using SellerCRUD.Infraestructure;
using SellerCRUD.Infraestructure.Data.Repositories;
using SellerCRUD.Services;
using SellerCRUD.Services.Common;
using SellerCRUD.Services.Interfaces;

namespace SellerCRUD.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ISellerService, SellerService>();
            services.AddScoped<ICityService, CityService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISellerRepository, SellerRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }

        public static void AddLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void AddDBConfiguration(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(
                option => option.UseSqlServer(connectionString)
                );
        }

        public static void AddCorsConfiguration(this IServiceCollection services, string MyAllowSpecificOrigins)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://172.168.13.117:4200");
                                      builder.WithOrigins("http://localhost:4200");
                                      builder.AllowAnyMethod();
                                      builder.AllowAnyHeader();
                                      builder.AllowAnyOrigin();
                                  });
            });
        }
    }
}
