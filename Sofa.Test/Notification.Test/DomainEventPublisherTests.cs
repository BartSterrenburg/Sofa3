using Sofa3.Domain.Notification;

namespace TestProject1.Notification.Test;

public class DomainEventPublisherTests
{
    private sealed class TestObserver : IDomainEventObserver
    {
        public List<DomainEvent> ReceivedEvents { get; } = new();

        public void Update(DomainEvent domainEvent)
        {
            ReceivedEvents.Add(domainEvent);
        }
    }

    private sealed class TestEvent : DomainEvent;

    [Test]
    public void Publish_stuurt_event_naar_gesubscribete_observer()
    {
        var publisher = new DomainEventPublisher();
        var observer = new TestObserver();
        var domainEvent = new TestEvent();

        publisher.Subscribe(observer);
        publisher.Publish(domainEvent);

        Assert.That(observer.ReceivedEvents, Has.Count.EqualTo(1));
        Assert.That(observer.ReceivedEvents[0], Is.SameAs(domainEvent));
    }

    [Test]
    public void Unsubscribe_stopt_verdere_notifications()
    {
        var publisher = new DomainEventPublisher();
        var observer = new TestObserver();
        var domainEvent = new TestEvent();

        publisher.Subscribe(observer);
        publisher.Unsubscribe(observer);
        publisher.Publish(domainEvent);

        Assert.That(observer.ReceivedEvents, Is.Empty);
    }

    [Test]
    public void Publish_stuurt_event_naar_alle_observers_in_volgorde_van_registratie()
    {
        var publisher = new DomainEventPublisher();
        var firstObserver = new TestObserver();
        var secondObserver = new TestObserver();
        var domainEvent = new TestEvent();

        publisher.Subscribe(firstObserver);
        publisher.Subscribe(secondObserver);
        publisher.Publish(domainEvent);

        Assert.Multiple(() =>
        {
            Assert.That(firstObserver.ReceivedEvents, Has.Count.EqualTo(1));
            Assert.That(secondObserver.ReceivedEvents, Has.Count.EqualTo(1));
            Assert.That(firstObserver.ReceivedEvents[0], Is.SameAs(domainEvent));
            Assert.That(secondObserver.ReceivedEvents[0], Is.SameAs(domainEvent));
        });
    }
}

