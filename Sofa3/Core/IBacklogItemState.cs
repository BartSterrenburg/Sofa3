using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Core
{
    public interface IBacklogItemState
    {
        void MoveToDoing(BacklogItem item);
        void MoveToReadyForTesting(BacklogItem item);
        void MoveToTesting(BacklogItem item);
        void MoveToTested(BacklogItem item);
        void MoveToDone(BacklogItem item);
    }
}
