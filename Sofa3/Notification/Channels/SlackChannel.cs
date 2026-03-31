using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Notification.Channels
{
    public class SlackChannel : INotificationChannel
    {
        public void Send(Notification notification)
        {
            Console.WriteLine($"[SLACK] {notification.Subject} - {notification.Message}");
        }
    }
}
