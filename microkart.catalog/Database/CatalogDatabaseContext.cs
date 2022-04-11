using Microsoft.EntityFrameworkCore;

namespace microkart.catalog.Database
{
    public class CatalogDatabaseContext : DbContext
    {
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductCatagory> ProductCatagories => Set<ProductCatagory>();

        public CatalogDatabaseContext(DbContextOptions<CatalogDatabaseContext> options)
            : base(options)
        {
        }
    }
}