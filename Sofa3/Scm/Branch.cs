using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Scm
{
    public class Branch
    {
        public string BranchName { get; private set; }
        public bool IsMain { get; private set; }

        public Branch(string branchName, bool isMain = false)
        {
            if (string.IsNullOrWhiteSpace(branchName))
                throw new ArgumentException("Branch name is required.", nameof(branchName));

            BranchName = branchName;
            IsMain = isMain;
        }
    }
}
