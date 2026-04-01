using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Notification
{
    public class NotificationService
    {
        private readonly INotificationChannel _notificationChannel;

        public NotificationService(INotificationChannel notificationChannel)
        {
            _notificationChannel = notificationChannel;
        }

        public void Send(Notification notification)
        {
            _notificationChannel.Send(notification);
        }  
    }
}
