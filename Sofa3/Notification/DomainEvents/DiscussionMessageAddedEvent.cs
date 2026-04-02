using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Notification.DomainEvents
{
    public class DiscussionMessageAddedEvent : DomainEvent
    {
        public String Message { get; private set; }
        public DiscussionMessageAddedEvent(String message)
        {
            Message = message;
        }
    }
}
