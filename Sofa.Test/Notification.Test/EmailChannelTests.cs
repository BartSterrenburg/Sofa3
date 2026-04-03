using System.IO;
using NotificationModel = Sofa3.Domain.Notification.Notification;
using Sofa3.Domain.Notification.Channels;

namespace TestProject1.Notification.Test;

public class EmailChannelTests
{
    [Test]
    public void Send_schrijft_email_bericht_naar_console()
    {
        var channel = new EmailChannel();
        var notification = new NotificationModel("Subject", "Message");
        var originalOut = Console.Out;
        using var writer = new StringWriter();

        try
        {
            Console.SetOut(writer);

            channel.Send(notification);

            Assert.That(writer.ToString().TrimEnd(), Is.EqualTo("[EMAIL] Subject - Message"));
        }
        finally
        {
            Console.SetOut(originalOut);
        }
    }
}


