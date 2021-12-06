using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductAPP.DBLayer.EF;
using System;

namespace ProductAPP.BLLayer.Services
{
    public class SeedDatabase
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var dataContext = serviceProvider.GetRequiredService<ApplicationContext>();
            dataContext.Database.Migrate();
        }
    }
}
