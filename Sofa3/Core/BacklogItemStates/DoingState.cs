using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Core.BacklogItemStates
{
    public class DoingState : IBacklogItemState
    {
        public void MoveToDoing(BacklogItem item)
        {
            throw new InvalidOperationException("Backlog item staat al op Doing.");
        }

        public void MoveToReadyForTesting(BacklogItem item)
        {
            item.MoveTo(new ReadyForTestingState());
        }

        public void MoveToTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Ga eerst naar ReadyForTesting.");
        }

        public void MoveToTested(BacklogItem item)
        {
            throw new InvalidOperationException("Kan niet direct van Doing naar Tested.");
        }

        public void MoveToDone(BacklogItem item)
        {
            throw new InvalidOperationException("Kan niet direct van Doing naar Done.");
        }
    }
}
