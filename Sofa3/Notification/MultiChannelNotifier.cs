using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Notification
{
    public class MultiChannelNotifier : INotificationChannel
    {
        private readonly List<INotificationChannel> _channels = new();

        public void AddChannel(INotificationChannel channel)
        {
            _channels.Add(channel);
        }

        public void RemoveChannel(INotificationChannel channel)
        {
            _channels.Remove(channel);
        }

        public void Send(Notification notification)
        {
            foreach (var channel in _channels)
            {
                channel.Send(notification);
            }
        }
    }
}
