using Sofa3.Domain.Notification.DomainEvents;

namespace TestProject1.Notification.Test;

public class DomainEventTests
{
    private sealed class TestDomainEvent : Sofa3.Domain.Notification.DomainEvent;

    [Test]
    public void DomainEvent_geeft_event_type_en_tijdstip_terug()
    {
        var before = DateTime.UtcNow;

        var domainEvent = new TestDomainEvent();

        var after = DateTime.UtcNow;

        Assert.Multiple(() =>
        {
            Assert.That(domainEvent.EventType, Is.EqualTo(nameof(TestDomainEvent)));
            Assert.That(domainEvent.OccurredAt, Is.InRange(before, after));
        });
    }

    [Test]
    public void SprintReleasedEvent_slaat_sprintId_op()
    {
        var sprintId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var domainEvent = new SprintReleasedEvent(sprintId);

        Assert.That(domainEvent.SprintId, Is.EqualTo(sprintId));
    }

    [Test]
    public void BacklogItemReturnedToToDoEvent_slaat_backlogItemId_op()
    {
        var backlogItemId = Guid.Parse("22222222-2222-2222-2222-222222222222");

        var domainEvent = new BacklogItemReturnedToToDoEvent(backlogItemId);

        Assert.That(domainEvent.BacklogItemId, Is.EqualTo(backlogItemId));
    }

    [Test]
    public void DiscussionMessageAddedEvent_slaat_message_op()
    {
        var domainEvent = new DiscussionMessageAddedEvent("New comment");

        Assert.That(domainEvent.Message, Is.EqualTo("New comment"));
    }
}

