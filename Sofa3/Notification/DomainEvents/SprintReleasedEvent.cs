using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Notification.DomainEvents
{
    public class SprintReleasedEvent : DomainEvent
    {
        public Guid SprintId { get; }

        public SprintReleasedEvent(Guid sprintId)
        {
            SprintId = sprintId;
        }
    }
}
