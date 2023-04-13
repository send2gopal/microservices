using microkart.shared.Database;
using System.ComponentModel.DataAnnotations;

namespace microkart.notification.Database
{
    public class NotificationTemplates
    {
        [Required]
        public int Id { get; set; }
        public string Type { get; set; } = "";
        public string Event { get; set; } = "";
        public string MessageTemplate { get; set; } = string.Empty;
        public string SubjectTemplate { get; set; } = string.Empty;
    }
}
