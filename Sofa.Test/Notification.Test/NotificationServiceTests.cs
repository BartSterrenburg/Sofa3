using NotificationModel = Sofa3.Domain.Notification.Notification;
using Sofa3.Domain.Notification;

namespace TestProject1.Notification.Test;

public class NotificationServiceTests
{
    private sealed class RecordingChannel : INotificationChannel
    {
        public List<NotificationModel> Notifications { get; } = new();

        public void Send(NotificationModel notification)
        {
            Notifications.Add(notification);
        }
    }

    [Test]
    public void Send_delegeert_naar_notification_channel()
    {
        var channel = new RecordingChannel();
        var service = new NotificationService(channel);
        var notification = new NotificationModel("Subject", "Message");

        service.Send(notification);

        Assert.That(channel.Notifications, Has.Count.EqualTo(1));
        Assert.That(channel.Notifications[0], Is.SameAs(notification));
    }
}


