using Microsoft.EntityFrameworkCore;
using ProductAPP.DBLayer.Entities;

namespace ProductAPP.DBLayer.EF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<ProductDb> Products { get; set; }
        public ApplicationContext() : base() { }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }
            return base.SaveChanges();
        }
    }
}
