using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Scm.Providers;
public class GitlabProvider : ScmProviderBase
{
    protected override string GetRepositoryName() => "GitLab Repo";
    protected override string GetDefaultBranch() => "master";

    protected override List<Branch> CreateBranches()
    {
        return new List<Branch>
        {
            new Branch("master", true),
            new Branch("staging"),
            new Branch("feature/payment")
        };
    }

    protected override List<Commit> CreateCommits(Branch branch)
    {
        return new List<Commit>
        {
            new Commit(Guid.NewGuid().ToString(), "Setup project", "Charlie", DateTime.UtcNow.AddDays(-3)),
            new Commit(Guid.NewGuid().ToString(), "Bugfix", "Dana", DateTime.UtcNow.AddDays(-1))
        };
    }
}
