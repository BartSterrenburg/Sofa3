using Sofa3.Domain.Core;

namespace TestProject1;

public class Fr010
{
    [Test]
    public void Gebruiker_met_rol_tester_wordt_correct_geregistreerd()
    {
        var project = new Project("Nieuw project", "Projectomschrijving");
        var user = new User(Guid.NewGuid(), "Test Tester", "tester@test.nl");

        project.AddMember(user, ProjectRole.TESTER);

        Assert.That(project.Members, Has.Count.EqualTo(1));

        var membership = project.Members.Single();
        Assert.That(membership.MembershipId, Is.Not.EqualTo(Guid.Empty));
        Assert.That(membership.UserId, Is.EqualTo(user.UserId));
        Assert.That(membership.Role, Is.EqualTo(ProjectRole.TESTER));
    }

    [Test]
    public void Gebruiker_met_rol_developer_wordt_correct_geregistreerd()
    {
        var project = new Project("Nieuw project", "Projectomschrijving");
        var user = new User(Guid.NewGuid(), "Dev Developer", "developer@test.nl");

        project.AddMember(user, ProjectRole.DEVELOPER);

        Assert.That(project.Members, Has.Count.EqualTo(1));

        var membership = project.Members.Single();
        Assert.That(membership.MembershipId, Is.Not.EqualTo(Guid.Empty));
        Assert.That(membership.UserId, Is.EqualTo(user.UserId));
        Assert.That(membership.Role, Is.EqualTo(ProjectRole.DEVELOPER));
    }

    [Test]
    public void Alleen_testers_ontvangen_testnotificatie()
    {
        var project = new Project("Nieuw project", "Projectomschrijving");

        var tester = new User(Guid.NewGuid(), "Tessa Tester", "tester@test.nl");
        var developer = new User(Guid.NewGuid(), "Daan Developer", "developer@test.nl");
        var scrumMaster = new User(Guid.NewGuid(), "Sam Scrum", "scrum@test.nl");

        project.AddMember(tester, ProjectRole.TESTER);
        project.AddMember(developer, ProjectRole.DEVELOPER);
        project.AddMember(scrumMaster, ProjectRole.SCRUM_MASTER);

        var testerRecipients = project.Members
            .Where(m => m.Role == ProjectRole.TESTER)
            .Select(m => m.UserId)
            .ToList();

        Assert.That(testerRecipients, Has.Count.EqualTo(1));
        Assert.That(testerRecipients, Contains.Item(tester.UserId));
        Assert.That(testerRecipients, Does.Not.Contain(developer.UserId));
        Assert.That(testerRecipients, Does.Not.Contain(scrumMaster.UserId));
    }
}