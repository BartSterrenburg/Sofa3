using Sofa3.Domain.Scm;
using System;
using System.Collections.Generic;

namespace TestProject1.Scm.Test;

public class ScmProviderBaseTests
{
    private sealed class TestScmProvider : ScmProviderBase
    {
        protected override string GetRepositoryName() => "Test Repository";
        protected override string GetDefaultBranch() => "main";

        protected override List<Branch> CreateBranches()
        {
            return new List<Branch>
            {
                new Branch("main", true),
                new Branch("develop")
            };
        }

        protected override List<Commit> CreateCommits(Branch branch)
        {
            return new List<Commit>
            {
                new Commit("abc123", $"Commit on {branch.BranchName}", "Bart", new DateTime(2026, 4, 3, 10, 0, 0, DateTimeKind.Utc))
            };
        }
    }

    [Test]
    public void GetRepository_geeft_repository_terug_met_waarden_uit_template_methods()
    {
        var provider = new TestScmProvider();

        var repository = provider.GetRepository("https://github.com/test/repo.git");

        Assert.Multiple(() =>
        {
            Assert.That(repository, Is.Not.Null);
            Assert.That(repository.Name, Is.EqualTo("Test Repository"));
            Assert.That(repository.RemoteUrl, Is.EqualTo("https://github.com/test/repo.git"));
        });
    }

    [Test]
    public void GetBranches_geeft_branches_terug_uit_createBranches()
    {
        var provider = new TestScmProvider();
        var repository = new ScmRepository("Repo", "https://github.com/test/repo.git", "main");

        var branches = provider.GetBranches(repository);

        Assert.Multiple(() =>
        {
            Assert.That(branches, Has.Count.EqualTo(2));

            Assert.That(branches[0].BranchName, Is.EqualTo("main"));
            Assert.That(branches[0].IsMain, Is.True);

            Assert.That(branches[1].BranchName, Is.EqualTo("develop"));
            Assert.That(branches[1].IsMain, Is.False);
        });
    }

    [Test]
    public void GetCommits_geeft_commits_terug_uit_createCommits()
    {
        var provider = new TestScmProvider();
        var branch = new Branch("feature/login");

        var commits = provider.GetCommits(branch);

        Assert.Multiple(() =>
        {
            Assert.That(commits, Has.Count.EqualTo(1));

            Assert.That(commits[0].CommitHash, Is.EqualTo("abc123"));
            Assert.That(commits[0].Message, Is.EqualTo("Commit on feature/login"));
            Assert.That(commits[0].AuthorName, Is.EqualTo("Bart"));
            Assert.That(commits[0].CommittedAt, Is.EqualTo(new DateTime(2026, 4, 3, 10, 0, 0, DateTimeKind.Utc)));
        });
    }
}