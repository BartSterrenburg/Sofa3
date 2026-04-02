using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Core.BacklogItemStates
{
    public class TestingState : IBacklogItemState
    {
        public void MoveToDoing(BacklogItem item)
        {
            item.MoveTo(new DoingState());
        }

        public void MoveToReadyForTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Backlog item zit al verder in het proces.");
        }

        public void MoveToTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Backlog item staat al op Testing.");
        }

        public void MoveToTested(BacklogItem item)
        {
            item.MoveTo(new TestedState());
        }

        public void MoveToDone(BacklogItem item)
        {
            throw new InvalidOperationException("Ga eerst naar Tested.");
        }
    }
}
