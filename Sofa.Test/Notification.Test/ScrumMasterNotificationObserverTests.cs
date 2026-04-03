using NotificationModel = Sofa3.Domain.Notification.Notification;
using Sofa3.Domain.Notification;
using Sofa3.Domain.Notification.DomainEvents;
using Sofa3.Domain.Notification.NotificationObservers;

namespace TestProject1.Notification.Test;

public class ScrumMasterNotificationObserverTests
{
    private sealed class RecordingChannel : INotificationChannel
    {
        public List<NotificationModel> Notifications { get; } = new();

        public void Send(NotificationModel notification)
        {
            Notifications.Add(notification);
        }
    }

    private sealed class TestDomainEvent : DomainEvent;

    [Test]
    public void Update_verstuurt_notificatie_bij_sprint_released_event()
    {
        var channel = new RecordingChannel();
        var observer = CreateObserver(channel);
        var sprintId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        observer.Update(new SprintReleasedEvent(sprintId));

        Assert.That(channel.Notifications, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(channel.Notifications[0].Subject, Is.EqualTo("Sprint released"));
            Assert.That(channel.Notifications[0].Message, Does.Contain(sprintId.ToString()));
        });
    }

    [Test]
    public void Update_verstuurt_notificatie_bij_backlog_item_returned_event()
    {
        var channel = new RecordingChannel();
        var observer = CreateObserver(channel);
        var backlogItemId = Guid.Parse("22222222-2222-2222-2222-222222222222");

        observer.Update(new BacklogItemReturnedToToDoEvent(backlogItemId));

        Assert.That(channel.Notifications, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(channel.Notifications[0].Subject, Is.EqualTo("Backlog item moved"));
            Assert.That(channel.Notifications[0].Message, Does.Contain(backlogItemId.ToString()));
            Assert.That(channel.Notifications[0].Message, Does.Contain(nameof(BacklogItemReturnedToToDoEvent)));
        });
    }

    [Test]
    public void Update_verstuurt_notificatie_bij_discussion_message_added_event()
    {
        var channel = new RecordingChannel();
        var observer = CreateObserver(channel);

        observer.Update(new DiscussionMessageAddedEvent("Thanks for the update"));

        Assert.That(channel.Notifications, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(channel.Notifications[0].Subject, Is.EqualTo("New discussion message"));
            Assert.That(channel.Notifications[0].Message, Does.Contain("Thanks for the update"));
        });
    }

    [Test]
    public void Update_doet_niets_bij_unsupported_event()
    {
        var channel = new RecordingChannel();
        var observer = CreateObserver(channel);

        observer.Update(new TestDomainEvent());

        Assert.That(channel.Notifications, Is.Empty);
    }

    private static ScrumMasterNotificationObserver CreateObserver(RecordingChannel channel)
    {
        return new ScrumMasterNotificationObserver(new NotificationService(channel));
    }
}


