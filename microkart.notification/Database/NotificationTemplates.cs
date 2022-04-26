using microkart.shared.Database;
using System.ComponentModel.DataAnnotations;

namespace microkart.notification.Database
{
    public class NotificationTemplates
    {
        [Required]
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        public string Type { get; set; } = "";
        public string Event { get; set; } = "";
        public string Message { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
    }
}
