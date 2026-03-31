using Sofa3.Domain.Notification.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Notification.NotificationObservers
{
    public class ScrumMasterNotificationObserver : IDomainEventObserver
    {
        private readonly NotificationService _notificationService;

        public ScrumMasterNotificationObserver(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public void Update(DomainEvent domainEvent)
        {
            if (domainEvent is SprintReleasedEvent sprintReleasedEvent)
            {
                var notification = new Notification(
                    "Sprint released",
                    $"Sprint with id {sprintReleasedEvent.SprintId} has been released."
                );

                _notificationService.Send(notification);
            }
        }
    }
}
