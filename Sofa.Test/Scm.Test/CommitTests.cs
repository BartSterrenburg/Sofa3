using Sofa3.Domain.Scm;

namespace TestProject1;

public class CommitTests
{
    [Test]
    public void Commit_wordt_succesvol_aangemaakt_met_geldige_waarden()
    {
        var committedAt = new DateTime(2026, 4, 3, 10, 30, 0, DateTimeKind.Utc);

        var commit = new Commit("abc123", "Initial commit", "Bart", committedAt);

        Assert.Multiple(() =>
        {
            Assert.That(commit.CommitHash, Is.EqualTo("abc123"));
            Assert.That(commit.Message, Is.EqualTo("Initial commit"));
            Assert.That(commit.AuthorName, Is.EqualTo("Bart"));
            Assert.That(commit.CommittedAt, Is.EqualTo(committedAt));
        });
    }

    [Test]
    public void Commit_gebruikt_lege_string_als_message_null_is()
    {
        var committedAt = DateTime.UtcNow;

        var commit = new Commit("abc123", null!, "Bart", committedAt);

        Assert.Multiple(() =>
        {
            Assert.That(commit.Message, Is.EqualTo(string.Empty));
        });
    }

    [Test]
    public void Commit_gebruikt_unknown_als_authorName_null_is()
    {
        var committedAt = DateTime.UtcNow;

        var commit = new Commit("abc123", "Bugfix", null!, committedAt);

        Assert.Multiple(() =>
        {
            Assert.That(commit.AuthorName, Is.EqualTo("unknown"));
        });
    }

    [Test]
    public void Commit_vereist_een_geldige_hash_bij_lege_string()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            new Commit("", "Bugfix", "Bart", DateTime.UtcNow));

        Assert.Multiple(() =>
        {
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception!.ParamName, Is.EqualTo("commitHash"));
            Assert.That(exception.Message, Does.StartWith("Commit hash is required."));
        });
    }

    [Test]
    public void Commit_vereist_een_geldige_hash_bij_whitespace()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            new Commit("   ", "Bugfix", "Bart", DateTime.UtcNow));

        Assert.Multiple(() =>
        {
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception!.ParamName, Is.EqualTo("commitHash"));
            Assert.That(exception.Message, Does.StartWith("Commit hash is required."));
        });
    }

    [Test]
    public void Commit_vereist_een_geldige_hash_bij_null()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            new Commit(null!, "Bugfix", "Bart", DateTime.UtcNow));

        Assert.Multiple(() =>
        {
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception!.ParamName, Is.EqualTo("commitHash"));
            Assert.That(exception.Message, Does.StartWith("Commit hash is required."));
        });
    }
}