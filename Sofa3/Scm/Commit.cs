using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Scm
{
    public class Commit
    {
        public string CommitHash { get; private set; }
        public string Message { get; private set; }
        public string AuthorName { get; private set; }
        public DateTime CommittedAt { get; private set; }

        public Commit(string commitHash, string message, string authorName, DateTime committedAt)
        {
            if (string.IsNullOrWhiteSpace(commitHash))
                throw new ArgumentException("Commit hash is required.", nameof(commitHash));

            CommitHash = commitHash;
            Message = message ?? string.Empty;
            AuthorName = authorName ?? "unknown";
            CommittedAt = committedAt;
        }
    }
}
