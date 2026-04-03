//using Sofa3.Domain.Core;

//namespace TestProject1;

//public class Fr001
//{
//    [Test]
//    public void Project_wordt_succesvol_aangemaakt_met_geldige_naam()
//    {
//        var project = new Project("Nieuw project", "Projectomschrijving");

//        Assert.That(project.ProjectId, Is.Not.EqualTo(Guid.Empty));
//        Assert.That(project.Name, Is.EqualTo("Nieuw project"));
//        Assert.That(project.Description, Is.EqualTo("Projectomschrijving"));
//        Assert.That(project.CreatedAt, Is.LessThanOrEqualTo(DateTime.UtcNow));
//    }

//    [Test]
//    public void Project_maakt_automatisch_een_product_backlog_aan()
//    {
//        var project = new Project("Nieuw project", "Projectomschrijving");

//        Assert.That(project.Backlog, Is.Not.Null);
//        Assert.That(project.Backlog.BacklogId, Is.Not.EqualTo(Guid.Empty));
//        Assert.That(project.Backlog.Name, Is.EqualTo("Product Backlog"));
//        Assert.That(project.Backlog.Items, Is.Empty);
//    }

//    [Test]
//    public void Project_vereist_een_geldige_naam()
//    {
//        var exception = Assert.Throws<ArgumentException>(() => new Project("   ", "Projectomschrijving"));

//        Assert.That(exception!.ParamName, Is.EqualTo("name"));
//        Assert.That(exception.Message, Does.StartWith("Project name is required."));
//    }
//}
