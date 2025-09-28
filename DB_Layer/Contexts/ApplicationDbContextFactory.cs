using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace merch_store.DB_Layer.Contexts
{
    // Фабрика нужна EF Core для миграций в design-time
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // ⚡️ ВСТАВЬ свой connection string
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=merch_store;Username=merch_admin;Password=admin123");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
