using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Scm
{
    public interface IScmProvider
    {
        ScmRepository GetRepository(string remoteUrl);
        List<Branch> GetBranches(ScmRepository repository);
        List<Commit> GetCommits(Branch branch);
    }
}
