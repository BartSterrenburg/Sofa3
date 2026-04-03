using Moq;
using Sofa3.Domain.Notification;
using NotificationModel = Sofa3.Domain.Notification.Notification;

namespace TestProject1.Notification.Test
{
    [TestFixture]
    public class MultiChannelNotifierTests
    {
        [Test]
        public void Send_stuurt_notification_naar_alle_kanalen()
        {
            var notifier = new MultiChannelNotifier();
            var firstChannelMock = new Mock<INotificationChannel>();
            var secondChannelMock = new Mock<INotificationChannel>();
            var notification = new NotificationModel("Subject", "Message");

            notifier.AddChannel(firstChannelMock.Object);
            notifier.AddChannel(secondChannelMock.Object);

            notifier.Send(notification);

            firstChannelMock.Verify(c => c.Send(notification), Times.Once);
            secondChannelMock.Verify(c => c.Send(notification), Times.Once);
        }

        [Test]
        public void RemoveChannel_stopt_verzending_naar_datzelfde_kanaal()
        {
            var notifier = new MultiChannelNotifier();
            var channelMock = new Mock<INotificationChannel>();
            var notification = new NotificationModel("Subject", "Message");

            notifier.AddChannel(channelMock.Object);
            notifier.RemoveChannel(channelMock.Object);

            notifier.Send(notification);

            channelMock.Verify(c => c.Send(It.IsAny<NotificationModel>()), Times.Never);
        }
    }
}