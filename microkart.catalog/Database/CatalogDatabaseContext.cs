using Microsoft.EntityFrameworkCore;

namespace microkart.catalog.Database
{
    public class CatalogDatabaseContext : DbContext
    {
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductCatagory> ProductCatagories => Set<ProductCatagory>();

        public DbSet<ProductImages> ProductImages => Set<ProductImages>();
        public DbSet<ProductVariant> ProductVariant => Set<ProductVariant>();

        public CatalogDatabaseContext(DbContextOptions<CatalogDatabaseContext> options)
            : base(options)
        {
        }
    }
}