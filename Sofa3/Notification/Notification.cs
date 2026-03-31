using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Notification
{
    public class Notification
    {
        public Guid NotificationId { get; }
        public string Subject { get; }
        public string Message { get; }
        public DateTime CreatedAt { get; }

        public Notification(string subject, string message)
        {
            NotificationId = Guid.NewGuid();
            Subject = subject;
            Message = message;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
