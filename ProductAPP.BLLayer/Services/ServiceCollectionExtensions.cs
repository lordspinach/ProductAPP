using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductAPP.BLLayer.MappingProfiles;
using ProductAPP.DBLayer.EF;
using ProductAPP.DBLayer.Interfaces;
using ProductAPP.DBLayer.Repositories;

namespace ProductAPP.BLLayer.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationContext>(
                options => options.UseNpgsql(connectionString,
                x => x.MigrationsAssembly("ProductAPP.DBLayer")));
        }
        public static void AddDbDependency(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
        }

        public static void AddDbLayerMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(ProductMappings)
            );
        }
    }
}
