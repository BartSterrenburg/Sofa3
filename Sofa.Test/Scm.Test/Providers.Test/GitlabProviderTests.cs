using Sofa3.Domain.Scm.Providers;
using Sofa3.Domain.Scm;
using System;
using NUnit.Framework;

namespace TestProject1.Scm.Test.Providers.Test
{
    public class GitlabProviderTests
    {
        [Test]
        public void GetRepository_geeft_een_gitlab_repository_terug()
        {
            var provider = new GitlabProvider();

            var repository = provider.GetRepository("https://gitlab.com/test/repo.git");

            Assert.Multiple(() =>
            {
                Assert.That(repository, Is.Not.Null);
                Assert.That(repository.RepositoryId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(repository.Name, Is.EqualTo("GitLab Repo"));
                Assert.That(repository.RemoteUrl, Is.EqualTo("https://gitlab.com/test/repo.git"));
                Assert.That(repository.DefaultBranchName, Is.EqualTo("master"));
            });
        }

        [Test]
        public void GetBranches_geeft_de_verwachte_gitlab_branches_terug()
        {
            var provider = new GitlabProvider();
            var repository = new ScmRepository("Repo", "https://gitlab.com/test/repo.git", "master");

            var branches = provider.GetBranches(repository);

            Assert.Multiple(() =>
            {
                Assert.That(branches, Has.Count.EqualTo(3));

                Assert.That(branches[0].BranchName, Is.EqualTo("master"));
                Assert.That(branches[0].IsMain, Is.True);

                Assert.That(branches[1].BranchName, Is.EqualTo("staging"));
                Assert.That(branches[1].IsMain, Is.False);

                Assert.That(branches[2].BranchName, Is.EqualTo("feature/payment"));
                Assert.That(branches[2].IsMain, Is.False);
            });
        }

        [Test]
        public void GetCommits_geeft_twee_commits_terug_met_verwachte_basisgegevens()
        {
            var provider = new GitlabProvider();
            var branch = new Branch("master", true);

            var before = DateTime.UtcNow.AddDays(-3).AddSeconds(-5);
            var commits = provider.GetCommits(branch);
            var after = DateTime.UtcNow.AddDays(-1).AddSeconds(5);

            Assert.Multiple(() =>
            {
                Assert.That(commits, Has.Count.EqualTo(2));

                Assert.That(commits[0].CommitHash, Is.Not.Null.And.Not.Empty);
                Assert.That(commits[0].Message, Is.EqualTo("Setup project"));
                Assert.That(commits[0].AuthorName, Is.EqualTo("Charlie"));
                Assert.That(commits[0].CommittedAt, Is.InRange(before, after));

                Assert.That(commits[1].CommitHash, Is.Not.Null.And.Not.Empty);
                Assert.That(commits[1].Message, Is.EqualTo("Bugfix"));
                Assert.That(commits[1].AuthorName, Is.EqualTo("Dana"));
                Assert.That(commits[1].CommittedAt, Is.InRange(before, after));
            });
        }

        [Test]
        public void GetCommits_genereert_unieke_commit_hashes()
        {
            var provider = new GitlabProvider();
            var branch = new Branch("staging");

            var commits = provider.GetCommits(branch);

            Assert.Multiple(() =>
            {
                Assert.That(commits, Has.Count.EqualTo(2));
                Assert.That(commits[0].CommitHash, Is.Not.EqualTo(commits[1].CommitHash));
            });
        }
    }
}