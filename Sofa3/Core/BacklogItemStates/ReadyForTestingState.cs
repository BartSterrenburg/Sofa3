using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Core.BacklogItemStates
{
    public class ReadyForTestingState : IBacklogItemState
    {
        public void MoveToDoing(BacklogItem item)
        {
            item.MoveTo(new DoingState());
        }

        public void MoveToReadyForTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Backlog item staat al op ReadyForTesting.");
        }

        public void MoveToTesting(BacklogItem item)
        {
            item.MoveTo(new TestingState());
        }

        public void MoveToTested(BacklogItem item)
        {
            throw new InvalidOperationException("Ga eerst naar Testing.");
        }

        public void MoveToDone(BacklogItem item)
        {
            throw new InvalidOperationException("Kan niet direct van ReadyForTesting naar Done.");
        }
    }
}
