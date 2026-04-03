using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Scm;
public abstract class ScmProviderBase : IScmProvider
{
    public ScmRepository GetRepository(string remoteUrl)
    {
        return new ScmRepository(
            GetRepositoryName(),
            remoteUrl,
            GetDefaultBranch()
        );
    }

    public List<Branch> GetBranches(ScmRepository repository)
    {
        return CreateBranches();
    }

    public List<Commit> GetCommits(Branch branch)
    {
        return CreateCommits(branch);
    }

    // Variabele delen → abstract
    protected abstract string GetRepositoryName();
    protected abstract string GetDefaultBranch();
    protected abstract List<Branch> CreateBranches();
    protected abstract List<Commit> CreateCommits(Branch branch);
}
