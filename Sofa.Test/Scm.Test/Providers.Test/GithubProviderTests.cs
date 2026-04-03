using Sofa3.Domain.Scm.Providers;
using Sofa3.Domain.Scm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.Scm.Test.Providers.Test
{
    public class GithubProviderTests
    {
        [Test]
        public void GetRepository_geeft_een_github_repository_terug()
        {
            var provider = new GithubProvider();

            var repository = provider.GetRepository("https://github.com/test/repo.git");

            Assert.Multiple(() =>
            {
                Assert.That(repository, Is.Not.Null);

                Assert.That(repository.RepositoryId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(repository.Name, Is.EqualTo("GitHub Repo"));
                Assert.That(repository.RemoteUrl, Is.EqualTo("https://github.com/test/repo.git"));
                Assert.That(repository.DefaultBranchName, Is.EqualTo("main"));
            });
        }

        [Test]
        public void GetBranches_geeft_de_verwachte_github_branches_terug()
        {
            var provider = new GithubProvider();
            var repository = new ScmRepository("Repo", "https://github.com/test/repo.git", "main");

            var branches = provider.GetBranches(repository);


            Assert.Multiple(() =>
            {
                Assert.That(branches, Has.Count.EqualTo(3));

                Assert.That(branches[0].BranchName, Is.EqualTo("main"));
                Assert.That(branches[0].IsMain, Is.True);

                Assert.That(branches[1].BranchName, Is.EqualTo("develop"));
                Assert.That(branches[1].IsMain, Is.False);

                Assert.That(branches[2].BranchName, Is.EqualTo("feature/login"));
                Assert.That(branches[2].IsMain, Is.False);
            });
        }

        [Test]
        public void GetCommits_geeft_twee_commits_terug_met_verwachte_basisgegevens()
        {
            var provider = new GithubProvider();
            var branch = new Branch("main", true);

            var before = DateTime.UtcNow.AddDays(-2).AddSeconds(-5);
            var commits = provider.GetCommits(branch);
            var after = DateTime.UtcNow.AddDays(-1).AddSeconds(5);


            Assert.Multiple(() =>
            {
                Assert.That(commits, Has.Count.EqualTo(2));

                Assert.That(commits[0].CommitHash, Is.Not.Null.And.Not.Empty);
                Assert.That(commits[0].Message, Is.EqualTo("Initial commit"));
                Assert.That(commits[0].AuthorName, Is.EqualTo("Alice"));
                Assert.That(commits[0].CommittedAt, Is.InRange(before, after));

                Assert.That(commits[1].CommitHash, Is.Not.Null.And.Not.Empty);
                Assert.That(commits[1].Message, Is.EqualTo("Added feature"));
                Assert.That(commits[1].AuthorName, Is.EqualTo("Bob"));
                Assert.That(commits[1].CommittedAt, Is.InRange(before, after));
            });
        }

        [Test]
        public void GetCommits_genereert_unieke_commit_hashes()
        {
            var provider = new GithubProvider();
            var branch = new Branch("develop");

            var commits = provider.GetCommits(branch);

            Assert.Multiple(() =>
            {
                Assert.That(commits, Has.Count.EqualTo(2));
                Assert.That(commits[0].CommitHash, Is.Not.EqualTo(commits[1].CommitHash));
            });
        }
    }
}