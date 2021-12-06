using Microsoft.EntityFrameworkCore;
using ProductAPP.DBLayer.Entities;

namespace ProductAPP.DBLayer.EF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<ProductDb> Products { get; set; }
        public ApplicationContext() : base() { }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    }
}
