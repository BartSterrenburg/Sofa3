using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Notification
{
    public class DomainEventPublisher
    {
        private readonly List<IDomainEventObserver> _observers = new();

        public void Subscribe(IDomainEventObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IDomainEventObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Publish(DomainEvent domainEvent)
        {
            foreach (var observer in _observers)
            {
                observer.Update(domainEvent);
            }
        }
    }
}
