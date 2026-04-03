using Sofa3.Domain.Core.BacklogItemStates;
using Sofa3.Domain.Notification;
using Sofa3.Domain.Notification.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Core
{
    public class BacklogItem : AggregateRoot
    {
        public Guid BacklogItemId { get; private set; }
        public Guid ProjectId { get; private set; }
        public Guid? SprintId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int StoryPoints { get; private set; }
        public bool IsLocked { get; private set; }
        public User? Owner { get; private set; }

        private readonly List<Activity> _activities = new();
        private readonly List<DiscussionThread> _discussionThreads = new();

        public IReadOnlyCollection<Activity> Activities => _activities.AsReadOnly();
        public IReadOnlyCollection<DiscussionThread> DiscussionThreads => _discussionThreads.AsReadOnly();

        public IBacklogItemState State { get; private set; }

        public BacklogItem(Guid projectId, string title, string description, int storyPoints, IBacklogItemState initialState)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Backlog item title is required.", nameof(title));
            }

            ArgumentNullException.ThrowIfNull(initialState);

            BacklogItemId = Guid.NewGuid();
            ProjectId = projectId;
            Title = title;
            Description = description;
            StoryPoints = storyPoints;
            IsLocked = false;
            State = initialState;
        }

        public void AssignDeveloper(User user)
        {
            this.Owner = user;
        }

        public void AddActivity(Activity activity)
        {
            ArgumentNullException.ThrowIfNull(activity);

            activity.LinkToBacklogItem(BacklogItemId);

            _activities.Add(activity);
        }

        public void AddDiscussionThread(string subject, object starter)
        {
            // In UML staat starter: User, maar die klasse staat niet in de foto.
            var thread = new DiscussionThread(subject);
            _discussionThreads.Add(thread);
        }

        internal void LinkToSprint(Guid sprintId)
        {
            if (SprintId.HasValue && SprintId.Value != sprintId)
            {
                throw new InvalidOperationException("Backlog item already belongs to a different sprint.");
            }

            SprintId = sprintId;
        }

        public void MoveTo(IBacklogItemState state)
        {
            if (state is ToDoState)
            {
                AddDomainEvent(new BacklogItemReturnedToToDoEvent(this.BacklogItemId));
            }
            State = state;
        }
    }
}
