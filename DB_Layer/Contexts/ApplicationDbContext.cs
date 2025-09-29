using merch_store.DB_Layer.DBTables;
using Microsoft.EntityFrameworkCore;

namespace merch_store.DB_Layer.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)");

                entity.Property(p => p.Name).IsRequired();
                entity.Property(p => p.ImageFront).IsRequired();
                entity.Property(p => p.ImageBack).IsRequired();
                entity.Property(p => p.Category).IsRequired();
                entity.Property(p => p.Type).IsRequired();
            });
        }
    }
}