using Sofa3.Domain.Scm;

namespace TestProject1;

public class BranchTests
{
    [Test]
    public void Branch_wordt_succesvol_aangemaakt_met_geldige_naam()
    {
        var branch = new Branch("feature/login");

        Assert.Multiple(() =>
        {
            Assert.That(branch.BranchName, Is.EqualTo("feature/login"));
            Assert.That(branch.IsMain, Is.False);
        });
    }

    [Test]
    public void Branch_wordt_succesvol_aangemaakt_als_main_branch()
    {
        var branch = new Branch("main", true);

        Assert.Multiple(() =>
        {
            Assert.That(branch.BranchName, Is.EqualTo("main"));
            Assert.That(branch.IsMain, Is.True);
        });
    }

    [Test]
    public void Branch_vereist_een_geldige_naam_bij_lege_string()
    {
        var exception = Assert.Throws<ArgumentException>(() => new Branch(""));

        Assert.Multiple(() =>
        {
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception!.ParamName, Is.EqualTo("branchName"));
            Assert.That(exception.Message, Does.StartWith("Branch name is required."));
        });
    }

    [Test]
    public void Branch_vereist_een_geldige_naam_bij_whitespace()
    {
        var exception = Assert.Throws<ArgumentException>(() => new Branch("   "));

        Assert.Multiple(() =>
        {
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception!.ParamName, Is.EqualTo("branchName"));
            Assert.That(exception.Message, Does.StartWith("Branch name is required."));
        });
    }

    [Test]
    public void Branch_vereist_een_geldige_naam_bij_null()
    {
        var exception = Assert.Throws<ArgumentException>(() => new Branch(null!));

        Assert.Multiple(() =>
        {
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception!.ParamName, Is.EqualTo("branchName"));
            Assert.That(exception.Message, Does.StartWith("Branch name is required."));
        });
    }
}