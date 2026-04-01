using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Scm.Providers
{
    public class GitlabProvider : IScmProvider
    {
        public ScmRepository GetRepository(string remoteUrl)
        {
            return new ScmRepository(
                "GitLab Repo",
                remoteUrl,
                "master"
            );
        }

        public List<Branch> GetBranches(ScmRepository repository)
        {
            return new List<Branch>
            {
                new Branch("master", true),
                new Branch("staging"),
                new Branch("feature/payment")
            };
        }

        public List<Commit> GetCommits(Branch branch)
        {
            return new List<Commit>
            {
                new Commit(Guid.NewGuid().ToString(), "Setup project", "Charlie", DateTime.UtcNow.AddDays(-3)),
                new Commit(Guid.NewGuid().ToString(), "Bugfix", "Dana", DateTime.UtcNow.AddDays(-1))
            };
        }
    }
}
