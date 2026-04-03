using Sofa3.Domain.Notification;

namespace TestProject1;

public class DomainEventTests
{
    private sealed class TestDomainEvent : DomainEvent
    {
    }

    [Test]
    public void DomainEvent_heeft_een_occurredAt_bij_aanmaak()
    {
        var before = DateTime.UtcNow;
        var domainEvent = new TestDomainEvent();
        var after = DateTime.UtcNow;

        Assert.That(domainEvent.OccurredAt, Is.GreaterThanOrEqualTo(before));
        Assert.That(domainEvent.OccurredAt, Is.LessThanOrEqualTo(after));
    }

    [Test]
    public void DomainEvent_geeft_het_juiste_eventtype_terug()
    {
        var domainEvent = new TestDomainEvent();

        Assert.That(domainEvent.EventType, Is.EqualTo(nameof(TestDomainEvent)));
    }
}
