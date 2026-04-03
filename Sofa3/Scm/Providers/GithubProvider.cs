using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Scm.Providers;
public class GithubProvider : ScmProviderBase
{
    protected override string GetRepositoryName() => "GitHub Repo";
    protected override string GetDefaultBranch() => "main";

    protected override List<Branch> CreateBranches()
    {
        return new List<Branch>
        {
            new Branch("main", true),
            new Branch("develop"),
            new Branch("feature/login")
        };
    }

    protected override List<Commit> CreateCommits(Branch branch)
    {
        return new List<Commit>
        {
            new Commit(Guid.NewGuid().ToString(), "Initial commit", "Alice", DateTime.UtcNow.AddDays(-2)),
            new Commit(Guid.NewGuid().ToString(), "Added feature", "Bob", DateTime.UtcNow.AddDays(-1))
        };
    }
}
