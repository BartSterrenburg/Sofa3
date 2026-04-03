//namespace TestProject1;

//public class Fr004
//{
//    [Test]
//    public void TC_014_Backlog_item_koppelen_aan_sprint_uit_zelfde_project_slaagt()
//    {
//        var project = new Sofa3.Domain.Core.Project("Project A", "Omschrijving");
//        var sprint = new Sofa3.Domain.Core.Sprint(Guid.NewGuid(), project.ProjectId, "Sprint 1");
//        var item = project.CreateBacklogItem("Zoeken", "Zoekfunctie");

//        sprint.AddBacklogItem(item);

//        Assert.That(item.SprintId, Is.EqualTo(sprint.SprintId));
//        Assert.That(sprint.BacklogItems, Contains.Item(item));
//    }

//    [Test]
//    public void TC_015_Backlog_item_koppelen_aan_sprint_uit_ander_project_geeft_fout()
//    {
//        var projectA = new Sofa3.Domain.Core.Project("Project A", "Omschrijving");
//        var projectB = new Sofa3.Domain.Core.Project("Project B", "Omschrijving");
//        var sprint = new Sofa3.Domain.Core.Sprint(Guid.NewGuid(), projectB.ProjectId, "Sprint B1");
//        var item = projectA.CreateBacklogItem("Import", "CSV import");

//        var exception = Assert.Throws<InvalidOperationException>(() => sprint.AddBacklogItem(item));

//        Assert.That(exception!.Message, Is.EqualTo("Backlog item belongs to a different project."));
//        Assert.That(item.SprintId, Is.Null);
//    }

//    [Test]
//    public void TC_016_Backlog_item_koppelen_terwijl_item_al_sprint_heeft_geeft_fout()
//    {
//        var project = new Sofa3.Domain.Core.Project("Project A", "Omschrijving");
//        var sprint1 = new Sofa3.Domain.Core.Sprint(Guid.NewGuid(), project.ProjectId, "Sprint 1");
//        var sprint2 = new Sofa3.Domain.Core.Sprint(Guid.NewGuid(), project.ProjectId, "Sprint 2");
//        var item = project.CreateBacklogItem("Notificaties", "Push meldingen");

//        sprint1.AddBacklogItem(item);
//        var exception = Assert.Throws<InvalidOperationException>(() => sprint2.AddBacklogItem(item));

//        Assert.That(exception!.Message, Is.EqualTo("Backlog item already belongs to a different sprint."));
//    }

//    [Test]
//    public void TC_017_Mislukte_tweede_koppeling_laat_item_bij_oorspronkelijke_sprint()
//    {
//        var project = new Sofa3.Domain.Core.Project("Project A", "Omschrijving");
//        var sprint1 = new Sofa3.Domain.Core.Sprint(Guid.NewGuid(), project.ProjectId, "Sprint 1");
//        var sprint2 = new Sofa3.Domain.Core.Sprint(Guid.NewGuid(), project.ProjectId, "Sprint 2");
//        var item = project.CreateBacklogItem("Rapportage", "Statusrapport");

//        sprint1.AddBacklogItem(item);
//        Assert.Throws<InvalidOperationException>(() => sprint2.AddBacklogItem(item));

//        Assert.That(item.SprintId, Is.EqualTo(sprint1.SprintId));
//        Assert.That(sprint1.BacklogItems, Contains.Item(item));
//        Assert.That(sprint2.BacklogItems, Does.Not.Contain(item));
//    }

//    [Test]
//    public void TC_018_Gekoppeld_backlog_item_krijgt_status_todo()
//    {
//        var project = new Sofa3.Domain.Core.Project("Project A", "Omschrijving");
//        var sprint = new Sofa3.Domain.Core.Sprint(Guid.NewGuid(), project.ProjectId, "Sprint 1");
//        var item = project.CreateBacklogItem("Dashboard", "Overzichtspagina");

//        item.MoveTo(new Sofa3.Domain.Core.BacklogItemStates.DoingState());
//        sprint.AddBacklogItem(item);

//        Assert.That(item.State, Is.TypeOf<Sofa3.Domain.Core.BacklogItemStates.ToDoState>());
//    }
//}