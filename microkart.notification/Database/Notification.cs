using microkart.shared.Database;
using System.ComponentModel.DataAnnotations;

namespace microkart.notification.Database
{
    public class Notification
    {
        [Required]
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        public string Type { get; set; } = "";
        public DateTime SentDate { get; set; }
        public string SentMessage { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Recipients { get; set; } = string.Empty;
    }
}
