using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Notification.DomainEvents
{
    public class BacklogItemReturnedToToDoEvent : DomainEvent
    {
        public Guid BacklogItemId { get; private set; }

        public BacklogItemReturnedToToDoEvent(Guid backlogItemId)
        {
            BacklogItemId = backlogItemId;
        }
    }
}
