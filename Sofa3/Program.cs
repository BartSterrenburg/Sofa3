// Main class

using Sofa3.Domain.Notification.Channels;
using Sofa3.Domain.Notification.NotificationObservers;
using Sofa3.Domain.Notification;
using Sofa3.Domain;


public class Program
{
    static void Main(string[] args)
    {
        // 1. Channels opzetten
        var emailChannel = new EmailChannel();
        var smsChannel = new SmsChannel();
        var slackChannel = new SlackChannel();

        // 2. MultiChannelNotifier configureren (Composite)
        var multiChannelNotifier = new MultiChannelNotifier();
        multiChannelNotifier.AddChannel(emailChannel);
        multiChannelNotifier.AddChannel(smsChannel);
        multiChannelNotifier.AddChannel(slackChannel);

        // 3. NotificationService (Facade)
        var notificationService = new NotificationService(multiChannelNotifier);

        // 4. Observer maken
        var scrumObserver = new ScrumMasterNotificationObserver(notificationService);

        // 5. Publisher maken en observer registreren
        var publisher = new DomainEventPublisher();
        publisher.Subscribe(scrumObserver);

        // 6. Sprint maken (inject publisher)
        var sprint = new Sprint(Guid.NewGuid(), "Sprint 1", publisher);

        // 7. Actie uitvoeren → triggert alles
        sprint.Release();

        Console.ReadLine();
    }
}