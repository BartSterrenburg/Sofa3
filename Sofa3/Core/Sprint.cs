using Sofa3.Domain.Notification.DomainEvents;
using Sofa3.Domain.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3.Domain.SprintReport;
using Sofa3.Domain.Core.SprintStates;

namespace Sofa3.Domain.Core
{
    public class Sprint : AggregateRoot
    {
        private readonly List<SprintReviewDocument> _reviewDocuments = new();

        public Guid SprintId { get; private set; }
        public string Name { get; private set; }
        public DateOnly StartDate { get; private set; }
        public DateOnly EndDate { get; private set; }
        public ISprintState CurrentState { get; private set; }


        public Sprint(Guid sprintId, string name, DateOnly startDate, DateOnly endDate)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Sprint name is required.", nameof(name));
            }

            if (endDate < startDate)
            {
                throw new ArgumentException("End date cannot be before start date.", nameof(endDate));
            }

            SprintId = sprintId;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            CurrentState = new ConceptSprintState();
        }

        public Sprint(Guid sprintId, string name)
            : this(
                sprintId,
                name,
                DateOnly.FromDateTime(DateTime.UtcNow),
                DateOnly.FromDateTime(DateTime.UtcNow.AddDays(14)))
        {
        }

        public void MoveTo(ISprintState state)
        {
            CurrentState = state ?? throw new ArgumentNullException(nameof(state));
        }

        public void Start() => CurrentState.Start(this);
        public void Finish() => CurrentState.Finish(this);
        public void StartRelease() => CurrentState.StartRelease(this);

        public void ReleaseSucceeded()
        {
            CurrentState.ReleaseSucceeded(this);

            if (CurrentState is ReleasedSprintState)
            {
                AddDomainEvent(new SprintReleasedEvent(SprintId));
            }
        }

        public void ReleaseFailed() => CurrentState.ReleaseFailed(this);
        public void Close() => CurrentState.Close(this);

        public void Release()
        {
            if (CurrentState is ConceptSprintState)
            {
                Start();
            }

            if (CurrentState is ActiveSprintState)
            {
                Finish();
            }

            if (CurrentState is FinishedSprintState || CurrentState is FailedSprintState)
            {
                StartRelease();
            }

            if (CurrentState is ReleasingSprintState)
            {
                ReleaseSucceeded();
            }
        }

        public SprintReviewDocument AddReviewDocument(string fileName, DateTime? uploadedAt = null)
        {
            var document = new SprintReviewDocument(Guid.NewGuid(), fileName, uploadedAt ?? DateTime.UtcNow);
            _reviewDocuments.Add(document);
            return document;
        }
    }
}