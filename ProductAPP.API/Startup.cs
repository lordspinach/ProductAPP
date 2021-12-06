using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using ProductAPP.BLLayer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProductAPP.API.MappingProfiles;
using ProductAPP.BLLayer.Interfaces;

namespace ProductAPP.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationContext();
            services.AddDbDependency();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IHttpService, HttpService>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductAPP.API", Version = "v1" });
            });
            services.AddDbLayerMapper();
            services.AddAutoMapper(
                typeof(ProductMappings)
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductAPP.API v1"));
            }

            var serviceProvider = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider;
            SeedDatabase.Initialize(serviceProvider);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
