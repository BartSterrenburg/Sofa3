using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Scm
{
    public class ScmRepository
    {
        public Guid RepositoryId { get; }
        public string Name { get; private set; }
        public string RemoteUrl { get; private set; }
        public string DefaultBranchName { get; private set; }

        public ScmRepository(string name, string remoteUrl, string defaultBranchName)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Repository name is required.", nameof(name));

            if (string.IsNullOrWhiteSpace(remoteUrl))
                throw new ArgumentException("Remote URL is required.", nameof(remoteUrl));

            RepositoryId = Guid.NewGuid();
            Name = name;
            RemoteUrl = remoteUrl;
            DefaultBranchName = defaultBranchName;
        }

        public Branch LinkBranch(string branchName)
        {
            if (string.IsNullOrWhiteSpace(branchName))
                throw new ArgumentException("Branch name is required.", nameof(branchName));

            return new Branch(branchName, branchName == DefaultBranchName);
        }

        public Commit LinkCommit(string commitHash)
        {
            if (string.IsNullOrWhiteSpace(commitHash))
                throw new ArgumentException("Commit hash is required.", nameof(commitHash));

            // In echte situatie haal je dit uit provider/API
            return new Commit(commitHash, "Linked commit", "system", DateTime.UtcNow);
        }
    }
}
