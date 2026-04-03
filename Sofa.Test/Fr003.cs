namespace TestProject1;

public class Fr003
{
    [Test]
    public void TC_009_Activiteit_aanmaken_met_titel_maakt_activiteit_aan()
    {
        var activity = new Sofa3.Domain.Core.Activity("Code review", "Review van PR");

        Assert.That(activity.ActivityId, Is.Not.EqualTo(Guid.Empty));
        Assert.That(activity.Title, Is.EqualTo("Code review"));
    }

    [Test]
    public void TC_010_Activiteit_aanmaken_zonder_titel_geeft_fout()
    {
        var exception = Assert.Throws<ArgumentException>(() => new Sofa3.Domain.Core.Activity("   ", "Omschrijving"));

        Assert.That(exception!.ParamName, Is.EqualTo("title"));
        Assert.That(exception.Message, Does.StartWith("Activity title is required."));
    }

    [Test]
    public void TC_011_Activiteit_koppelen_aan_backlog_item_wordt_gekoppeld()
    {
        var backlogItem = CreateBacklogItem();
        var activity = new Sofa3.Domain.Core.Activity("Implementatie", "Feature bouwen");

        backlogItem.AddActivity(activity);

        Assert.That(backlogItem.Activities, Contains.Item(activity));
        Assert.That(activity.BacklogItemId, Is.EqualTo(backlogItem.BacklogItemId));
    }

    [Test]
    public void TC_012_Activiteit_hoort_bij_exact_een_backlog_item()
    {
        var backlogItemA = CreateBacklogItem();
        var backlogItemB = CreateBacklogItem();
        var activity = new Sofa3.Domain.Core.Activity("Testen", "Unit tests toevoegen");

        backlogItemA.AddActivity(activity);
        var exception = Assert.Throws<InvalidOperationException>(() => backlogItemB.AddActivity(activity));

        Assert.That(exception!.Message, Is.EqualTo("Activity already belongs to a different backlog item."));
        Assert.That(activity.BacklogItemId, Is.EqualTo(backlogItemA.BacklogItemId));
        Assert.That(backlogItemB.Activities, Does.Not.Contain(activity));
    }

    [Test]
    public void TC_013_Nieuwe_activiteit_krijgt_standaardstatus_todo()
    {
        var activity = new Sofa3.Domain.Core.Activity("Documenteren", "API documentatie");

        Assert.That(activity.Status, Is.EqualTo(Sofa3.Domain.Core.ActivityStatus.TODO));
    }

    private static Sofa3.Domain.Core.BacklogItem CreateBacklogItem()
    {
        var project = new Sofa3.Domain.Core.Project("Project X", "Omschrijving");
        return project.CreateBacklogItem("Backlog item", "Details");
    }
}