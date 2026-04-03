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
        public Guid? BacklogItemId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public ActivityStatus Status { get; private set; }

        public Activity(string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Activity title is required.", nameof(title));
            }

            ActivityId = Guid.NewGuid();
            Title = title;
            Description = description ?? string.Empty;
            Status = ActivityStatus.TODO;
        }

        internal void LinkToBacklogItem(Guid backlogItemId)
        {
            if (BacklogItemId.HasValue && BacklogItemId.Value != backlogItemId)
            {
                throw new InvalidOperationException("Activity already belongs to a different backlog item.");
            }

            BacklogItemId = backlogItemId;
        }

        public void MarkDone()
        {
            Status = ActivityStatus.DONE;
        }
    }
}
