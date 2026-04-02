using Sofa3.Domain.Notification;
using Sofa3.Domain.Notification.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Core
{
    public class Sprint
    {
        public Guid SprintId { get; private set; }
        public string Name { get; private set; }
        public DateOnly StartDate { get; private set; }
        public DateOnly EndDate { get; private set; }

        private readonly DomainEventPublisher _eventPublisher;

        public Sprint(Guid sprintId, string name, DomainEventPublisher eventPublisher)
        {
            SprintId = sprintId;
            Name = name;
            _eventPublisher = eventPublisher;
        }

        public void Release()
        {
            var domainEvent = new SprintReleasedEvent(SprintId);

            _eventPublisher.Publish(domainEvent);
        }
    }
}
