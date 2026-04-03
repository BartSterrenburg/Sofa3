using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Core.BacklogItemStates
{
    public class DoneState : IBacklogItemState
    {
        private static String exceptionMessage = "Done is the endstatus.";
        public void MoveToDoing(BacklogItem item)
        {
            throw new InvalidOperationException(exceptionMessage);
        }

        public void MoveToReadyForTesting(BacklogItem item)
        {
            throw new InvalidOperationException(exceptionMessage);
        }

        public void MoveToTesting(BacklogItem item)
        {
            throw new InvalidOperationException(exceptionMessage);
        }

        public void MoveToTested(BacklogItem item)
        {
            throw new InvalidOperationException(exceptionMessage);
        }

        public void MoveToDone(BacklogItem item)
        {
            throw new InvalidOperationException("Backlog item staat al op Done.");
        }
    }
}
