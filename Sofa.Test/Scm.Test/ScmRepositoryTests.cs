using Sofa3.Domain.Scm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.Scm.Test
{
    public class ScmRepositoryTests
    {
        [Test]
        public void Repository_wordt_succesvol_aangemaakt_met_geldige_waarden()
        {
            var repo = new ScmRepository("Repo", "https://github.com/test/repo.git", "main");

            Assert.That(repo.RepositoryId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(repo.Name, Is.EqualTo("Repo"));
            Assert.That(repo.RemoteUrl, Is.EqualTo("https://github.com/test/repo.git"));
            Assert.That(repo.DefaultBranchName, Is.EqualTo("main"));
        }

        [Test]
        public void Repository_vereist_een_geldige_naam()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new ScmRepository("", "https://github.com/test/repo.git", "main"));

            Assert.That(ex!.ParamName, Is.EqualTo("name"));
            Assert.That(ex.Message, Does.StartWith("Repository name is required."));
        }

        [Test]
        public void Repository_vereist_een_geldige_remoteUrl()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new ScmRepository("Repo", "   ", "main"));

            Assert.That(ex!.ParamName, Is.EqualTo("remoteUrl"));
            Assert.That(ex.Message, Does.StartWith("Remote URL is required."));
        }

        [Test]
        public void LinkBranch_geeft_main_branch_correct_terug()
        {
            var repo = new ScmRepository("Repo", "url", "main");

            var branch = repo.LinkBranch("main");

            Assert.That(branch.BranchName, Is.EqualTo("main"));
            Assert.That(branch.IsMain, Is.True);
        }

        [Test]
        public void LinkBranch_geeft_non_main_branch_correct_terug()
        {
            var repo = new ScmRepository("Repo", "url", "main");

            var branch = repo.LinkBranch("feature/login");

            Assert.That(branch.BranchName, Is.EqualTo("feature/login"));
            Assert.That(branch.IsMain, Is.False);
        }

        [Test]
        public void LinkBranch_vereist_geldige_naam()
        {
            var repo = new ScmRepository("Repo", "url", "main");

            var ex = Assert.Throws<ArgumentException>(() =>
                repo.LinkBranch(""));

            Assert.That(ex!.ParamName, Is.EqualTo("branchName"));
            Assert.That(ex.Message, Does.StartWith("Branch name is required."));
        }

        [Test]
        public void LinkCommit_geeft_commit_met_standaardwaarden()
        {
            var commit = ScmRepository.LinkCommit("abc123");

            Assert.That(commit.CommitHash, Is.EqualTo("abc123"));
            Assert.That(commit.Message, Is.EqualTo("Linked commit"));
            Assert.That(commit.AuthorName, Is.EqualTo("system"));
            Assert.That(commit.CommittedAt, Is.LessThanOrEqualTo(DateTime.UtcNow));
        }

        [Test]
        public void LinkCommit_vereist_geldige_hash()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                ScmRepository.LinkCommit("   "));

            Assert.That(ex!.ParamName, Is.EqualTo("commitHash"));
            Assert.That(ex.Message, Does.StartWith("Commit hash is required."));
        }
    }
}