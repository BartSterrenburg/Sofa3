using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Scm.Providers
{
    public class GithubProvider : IScmProvider
    {
        public ScmRepository GetRepository(string remoteUrl)
        {
            return new ScmRepository(
                "GitHub Repo",
                remoteUrl,
                "main"
            );
        }

        public List<Branch> GetBranches(ScmRepository repository)
        {
            return new List<Branch>
            {
                new Branch("main", true),
                new Branch("develop"),
                new Branch("feature/login")
            };
        }

        public List<Commit> GetCommits(Branch branch)
        {
            return new List<Commit>
            {
                new Commit(Guid.NewGuid().ToString(), "Initial commit", "Alice", DateTime.UtcNow.AddDays(-2)),
                new Commit(Guid.NewGuid().ToString(), "Added feature", "Bob", DateTime.UtcNow.AddDays(-1))
            };
        }
    }
}
