using Microsoft.EntityFrameworkCore;

namespace microkart.notification.Database
{
    public class NotificationDatabaseContext : DbContext
    {
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<NotificationTemplates> NotificationTemplates => Set<NotificationTemplates>();
        public NotificationDatabaseContext(DbContextOptions<NotificationDatabaseContext> options)
            : base(options)
        {

        }
    }
}