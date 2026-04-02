using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Core
{
    public class ProjectMembership
    {
        public Guid MembershipId { get; private set; }
        public Guid UserId { get; private set; }
        public ProjectRole Role { get; private set; }

        public ProjectMembership(Guid userId, ProjectRole role)
        {
            MembershipId = Guid.NewGuid();
            UserId = userId;
            Role = role;
        }

    }
}
