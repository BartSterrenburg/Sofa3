//namespace TestProject1;

//public class Fr002
//{
//    [Test]
//    public void TC_004_Backlog_item_aanmaken_met_titel_maakt_item_aan()
//    {
//        var project = new Sofa3.Domain.Core.Project("Project A", "Omschrijving");

//        var item = project.CreateBacklogItem("Inlogscherm", "Gebruiker kan inloggen");

//        Assert.That(item.Title, Is.EqualTo("Inlogscherm"));
//        Assert.That(project.Backlog.Items, Contains.Item(item));
//    }

//    [Test]
//    public void TC_005_Backlog_item_aanmaken_zonder_titel_geeft_fout()
//    {
//        var project = new Sofa3.Domain.Core.Project("Project A", "Omschrijving");

//        var exception = Assert.Throws<ArgumentException>(() => project.CreateBacklogItem("   ", "Omschrijving"));

//        Assert.That(exception!.ParamName, Is.EqualTo("title"));
//        Assert.That(exception.Message, Does.StartWith("Backlog item title is required."));
//    }

//    [Test]
//    public void TC_006_Backlog_item_hoort_bij_exact_een_project()
//    {
//        var projectA = new Sofa3.Domain.Core.Project("Project A", "Omschrijving");
//        var projectB = new Sofa3.Domain.Core.Project("Project B", "Omschrijving");
//        var item = projectA.CreateBacklogItem("Zoeken", "Zoekfunctionaliteit");

//        var exception = Assert.Throws<InvalidOperationException>(() => projectB.Backlog.AddItem(item));

//        Assert.That(exception!.Message, Is.EqualTo("Backlog item belongs to a different project."));
//        Assert.That(projectA.Backlog.Items, Contains.Item(item));
//        //Assert.That(projectB.Backlog.Items, Does.Not.Contain(item));
//    }

//    [Test]
//    public void TC_007_Backlog_item_wordt_gekoppeld_aan_juiste_project()
//    {
//        var project = new Sofa3.Domain.Core.Project("Project A", "Omschrijving");

//        var item = project.CreateBacklogItem("Autorisatie", "Rollen en rechten");

//        Assert.That(item.ProjectId, Is.EqualTo(project.ProjectId));
//        Assert.That(project.Backlog.ProjectId, Is.EqualTo(project.ProjectId));
//        Assert.That(project.Backlog.Items, Contains.Item(item));
//    }

//    [Test]
//    public void TC_008_Nieuw_backlog_item_staat_standaard_in_product_backlog_van_project()
//    {
//        var project = new Sofa3.Domain.Core.Project("Project A", "Omschrijving");

//        var item = project.CreateBacklogItem("Notificaties", "Meldingen tonen");

//        Assert.That(project.Backlog.Name, Is.EqualTo("Product Backlog"));
//        Assert.That(project.Backlog.Items.Count, Is.EqualTo(1));
//        Assert.That(project.Backlog.Items[0], Is.EqualTo(item));
//    }
//}