using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Core
{
    public class BacklogItem
    {
        public Guid BacklogItemId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int StoryPoints { get; private set; }
        public bool IsLocked { get; private set; }

        private readonly List<Activity> _activities = new();
        private readonly List<DiscussionThread> _discussionThreads = new();

        public IReadOnlyCollection<Activity> Activities => _activities.AsReadOnly();
        public IReadOnlyCollection<DiscussionThread> DiscussionThreads => _discussionThreads.AsReadOnly();

        public IBacklogItemState State { get; private set; }

        public BacklogItem(string title, string description, int storyPoints, IBacklogItemState initialState)
        {
            BacklogItemId = Guid.NewGuid();
            Title = title;
            Description = description;
            StoryPoints = storyPoints;
            IsLocked = false;
            State = initialState;
        }

        public void AssignDeveloper(object user)
        {
            // In UML staat User, maar die klasse staat niet in de foto.
        }

        public void AddActivity(Activity activity)
        {
            if (activity == null)
                throw new ArgumentNullException(nameof(activity));

            _activities.Add(activity);
        }

        public void AddDiscussionThread(string subject, object starter)
        {
            // In UML staat starter: User, maar die klasse staat niet in de foto.
            var thread = new DiscussionThread(subject);
            _discussionThreads.Add(thread);
        }

        public void MoveTo(IBacklogItemState state)
        {
            State = state;
        }
    }
}
