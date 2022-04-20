using Microsoft.EntityFrameworkCore;

namespace microkart.basket.Database
{
    public class BasketDatabaseContext : DbContext
    {
        public DbSet<CartItem> CartItems => Set<CartItem>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<Order> Orders => Set<Order>();

        public BasketDatabaseContext(DbContextOptions<BasketDatabaseContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Cart>()
                .HasMany(e => e.Items)
                .WithOne(e => e.Cart)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}