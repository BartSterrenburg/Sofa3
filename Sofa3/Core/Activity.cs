using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Core
{
    using System;

    public class Activity
    {
        public Guid ActivityId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public ActivityStatus Status { get; private set; }

        public Activity(string title, string description)
        {
            ActivityId = Guid.NewGuid();
            Title = title;
            Description = description;
            Status = ActivityStatus.TODO;
        }

        public void MarkDone()
        {
            Status = ActivityStatus.DONE;
        }
    }
}
