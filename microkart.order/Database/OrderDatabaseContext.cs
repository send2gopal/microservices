using Microsoft.EntityFrameworkCore;

namespace microkart.order.Database
{
    public class OrderDatabaseContext : DbContext
    {
        public DbSet<OrderEntity> Orders => Set<OrderEntity>();

        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<ShippingAddress> ShippingAddresses => Set<ShippingAddress>();

        public OrderDatabaseContext(DbContextOptions<OrderDatabaseContext> options)
            : base(options)
        {

        }
    }
}