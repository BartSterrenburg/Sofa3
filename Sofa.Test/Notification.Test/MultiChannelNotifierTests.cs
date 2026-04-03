using NotificationModel = Sofa3.Domain.Notification.Notification;
using Sofa3.Domain.Notification;

namespace TestProject1.Notification.Test;

public class MultiChannelNotifierTests
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
    public void Send_stuurt_notification_naar_alle_kanalen()
    {
        var notifier = new MultiChannelNotifier();
        var firstChannel = new RecordingChannel();
        var secondChannel = new RecordingChannel();
        var notification = new NotificationModel("Subject", "Message");

        notifier.AddChannel(firstChannel);
        notifier.AddChannel(secondChannel);
        notifier.Send(notification);

        Assert.Multiple(() =>
        {
            Assert.That(firstChannel.Notifications, Has.Count.EqualTo(1));
            Assert.That(secondChannel.Notifications, Has.Count.EqualTo(1));
            Assert.That(firstChannel.Notifications[0], Is.SameAs(notification));
            Assert.That(secondChannel.Notifications[0], Is.SameAs(notification));
        });
    }

    [Test]
    public void RemoveChannel_stopt_verzending_naar_datzelfde_kanaal()
    {
        var notifier = new MultiChannelNotifier();
        var channel = new RecordingChannel();
        var notification = new NotificationModel("Subject", "Message");

        notifier.AddChannel(channel);
        notifier.RemoveChannel(channel);
        notifier.Send(notification);

        Assert.That(channel.Notifications, Is.Empty);
    }
}


