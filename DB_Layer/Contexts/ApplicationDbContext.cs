//using DB_Layer.DBTables;
//using Microsoft.EntityFrameworkCore;

//namespace DB_Layer.Contexts
//{
//    public class ApplicationDbContext : DbContext
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts) : base(opts) { }

//        public DbSet<Product> Products { get; set; } = null!;

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            modelBuilder.Entity<Product>(entity =>
//            {
//                entity.ToTable("Products");

//                entity.Property(p => p.Name)
//                      .IsRequired()
//                      .HasMaxLength(200);

//                entity.Property(p => p.Price)
//                      .HasPrecision(10, 2); // <-- рекомендую

//                entity.Property(p => p.ImageFront).HasMaxLength(500);
//                entity.Property(p => p.ImageBack).HasMaxLength(500);
//                entity.Property(p => p.Category).HasMaxLength(100);
//            });
//        }
//    }
//}

using DB_Layer.DBTables;
using Microsoft.EntityFrameworkCore;

namespace DB_Layer.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
