using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Core.BacklogItemStates
{
    public class ToDoState : IBacklogItemState
    {
        public void MoveToDoing(BacklogItem item)
        {
            item.MoveTo(new DoingState());
        }

        public void MoveToReadyForTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Kan niet direct van ToDo naar ReadyForTesting.");
        }

        public void MoveToTesting(BacklogItem item)
        {
            throw new InvalidOperationException("Kan niet direct van ToDo naar Testing.");
        }

        public void MoveToTested(BacklogItem item)
        {
            throw new InvalidOperationException("Kan niet direct van ToDo naar Tested.");
        }

        public void MoveToDone(BacklogItem item)
        {
            throw new InvalidOperationException("Kan niet direct van ToDo naar Done.");
        }
    }
}
