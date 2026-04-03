using NotificationModel = Sofa3.Domain.Notification.Notification;

namespace TestProject1.Notification.Test;

public class NotificationTests
{
    [Test]
    public void Notification_wordt_succesvol_aangemaakt_met_geldige_waarden()
    {
        var before = DateTime.UtcNow;

        var notification = new NotificationModel("Build failed", "The pipeline failed.");

        var after = DateTime.UtcNow;

        Assert.Multiple(() =>
        {
            Assert.That(notification.NotificationId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(notification.Subject, Is.EqualTo("Build failed"));
            Assert.That(notification.Message, Is.EqualTo("The pipeline failed."));
            Assert.That(notification.CreatedAt, Is.InRange(before, after));
        });
    }
}


