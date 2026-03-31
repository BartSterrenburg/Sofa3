using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Notification.Channels
{
    public class SmsChannel : INotificationChannel
    {
        public void Send(Notification notification)
        {
            Console.WriteLine($"[SMS] {notification.Subject} - {notification.Message}");
        }
    }
}
