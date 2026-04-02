using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Core.BacklogItemStates
{
    public class DoneState : IBacklogItemState
    {
        public void MoveToDoing(BacklogItem item)
        {
            throw new InvalidOperationException("Done is een eindstatus.");
        }

        public void MoveToReadyForTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Done is een eindstatus.");
        }

        public void MoveToTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Done is een eindstatus.");
        }

        public void MoveToTested(BacklogItem item)
        {
            throw new InvalidOperationException("Done is een eindstatus.");
        }

        public void MoveToDone(BacklogItem item)
        {
            throw new InvalidOperationException("Backlog item staat al op Done.");
        }
    }
}
