// Main class

using Sofa3.Domain.Notification.Channels;
using Sofa3.Domain.Notification.NotificationObservers;
using Sofa3.Domain.Notification;
using Sofa3.Domain.Pipeline;
using Sofa3.Domain.Pipeline.Enumerations;
using Sofa3.Domain.Scm.Providers;
using Sofa3.Domain.Scm;
using Sofa3.Domain.Core;
using Sofa3.Domain.Core.BacklogItemStates;

namespace Sofa3.Domain;
public class Program
{
    public static void Main(string[] args)
    {

        // 1. Channels opzetten
        var emailChannel = new EmailChannel();
        var smsChannel = new SmsChannel();

        // 2. MultiChannelNotifier configureren (Composite)
        var multiChannelNotifier = new MultiChannelNotifier();
        multiChannelNotifier.AddChannel(emailChannel);
        multiChannelNotifier.AddChannel(smsChannel);

        // 3. NotificationService (Facade)
        var notificationService = new NotificationService(multiChannelNotifier);

        var publisher = new DomainEventPublisher();

        var scrumObserver = new ScrumMasterNotificationObserver(notificationService);

        // voorbeeld observer subscriben
        publisher.Subscribe(scrumObserver);


        DiscussionThread discussion = new DiscussionThread("Discussie over API design");
        discussion.AddMessage(new Message(
            "Ik denk dat we REST moeten gebruiken."));

        foreach (var domainEvent in discussion.DomainEvents)
        {
            publisher.Publish(domainEvent);
        }

        discussion.ClearDomainEvents();

    }

    public static void TestScm()
    {
        IScmProvider provider = new GithubProvider();

        var repo = provider.GetRepository("https://github.com/BartSterrenburg/Sofa3.git");
        var branches = provider.GetBranches(repo);

        Console.WriteLine($"Repo: {repo.Name}");

        foreach (var branch in branches)
        {
            Console.WriteLine($"Branch: {branch.BranchName}");

            var commits = provider.GetCommits(branch);
            foreach (var commit in commits)
            {
                Console.WriteLine($"  - {commit.CommitHash} | {commit.Message}");
            }
        }
    }
    public static void TestPipeline()
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
        var sprint = new Sprint(Guid.NewGuid(), "Sprint 1");

        // Project aanmaken
        var project = new Project(
            "Call-a-Car Platform",
            "Platform voor backlogbeheer en development pipelines."
        );

        Console.WriteLine("Project aangemaakt:");
        Console.WriteLine($"- Id: {project.ProjectId}");
        Console.WriteLine($"- Naam: {project.Name}");
        Console.WriteLine($"- Beschrijving: {project.Description}");
        Console.WriteLine($"- CreatedAt: {project.CreatedAt}");
        Console.WriteLine();


        Console.WriteLine("Sprint aangemaakt:");
        Console.WriteLine($"- Id: {sprint.SprintId}");
        Console.WriteLine($"- Naam: {sprint.Name}");
        Console.WriteLine($"- Start: {sprint.StartDate}");
        Console.WriteLine($"- Einde: {sprint.EndDate}");
        Console.WriteLine();

        // Pipeline aanmaken
        var pipeline = new Pipeline(
            "CI Pipeline",
            PipelineTriggerMode.AUTOMATIC
        );

        Console.WriteLine("Pipeline aangemaakt:");
        Console.WriteLine($"- Id: {pipeline.PipelineId}");
        Console.WriteLine($"- Naam: {pipeline.Name}");
        Console.WriteLine($"- TriggerMode: {pipeline.TriggerMode}");
        Console.WriteLine();

        // Steps toevoegen
        pipeline.AddStep(new PipelineStep("Checkout Sources", 1, PipelineStepType.SOURCES));
        pipeline.AddStep(new PipelineStep("Create Package", 2, PipelineStepType.PACKAGE));
        pipeline.AddStep(new PipelineStep("Build Application", 3, PipelineStepType.BUILD));
        pipeline.AddStep(new PipelineStep("Run Tests", 4, PipelineStepType.TEST));
        pipeline.AddStep(new PipelineStep("Static Analysis", 5, PipelineStepType.ANALYSE));
        pipeline.AddStep(new PipelineStep("Deploy to Test", 6, PipelineStepType.DEPLOY));
        pipeline.AddStep(new PipelineStep("Cleanup Utility", 7, PipelineStepType.UTILITY));

        Console.WriteLine("Pipeline steps:");
        foreach (var step in pipeline.Steps)
        {
            Console.WriteLine($"- [{step.Order}] {step.Name} ({step.Type})");
        }

        Console.WriteLine();
        Console.WriteLine("Pipeline wordt uitgevoerd...");
        Console.WriteLine();

        // Execution starten
        var execution = pipeline.CreateExecution();

        Console.WriteLine("Pipeline execution resultaat:");
        Console.WriteLine($"- ExecutionId: {execution.ExecutionId}");
        Console.WriteLine($"- StartedAt: {execution.StartedAt}");
        Console.WriteLine($"- FinishedAt: {execution.FinishedAt}");
        Console.WriteLine($"- Status: {execution.Status}");
        Console.WriteLine();

        Console.WriteLine("Step executions:");
        foreach (var stepExecution in execution.StepExecutions)
        {
            Console.WriteLine($"- StepExecutionId: {stepExecution.StepExecutionId}");
            Console.WriteLine($"  StartedAt: {stepExecution.StartedAt}");
            Console.WriteLine($"  FinishedAt: {stepExecution.FinishedAt}");
            Console.WriteLine($"  Status: {stepExecution.Status}");
            Console.WriteLine($"  Log: {stepExecution.LogMessage}");
            Console.WriteLine();
        }

        Console.WriteLine("Demo afgerond.");
        Console.ReadLine();
    }
    public static void TestNotificationService()
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
        var sprint = new Sprint(Guid.NewGuid(), "Sprint 1");

        // 7. Actie uitvoeren → triggert alles
        sprint.Release();

        Console.ReadLine();
    }
}

