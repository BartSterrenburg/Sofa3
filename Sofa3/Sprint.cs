using Sofa3.Domain.Notification;
using Sofa3.Domain.Notification.DomainEvents;
using Sofa3.Domain.SprintLifecycle;
using Sofa3.Domain.SprintLifecycle.States;
using Sofa3.Domain.SprintReview;
using System;
using System.Collections.Generic;

namespace Sofa3.Domain
{
    public class Sprint
    {
        private readonly DomainEventPublisher _eventPublisher;
        private readonly List<SprintReviewDocument> _reviewDocuments = new();

        public Guid SprintId { get; private set; }
        public string Name { get; private set; }
        public DateOnly StartDate { get; private set; }
        public DateOnly EndDate { get; private set; }
        public SprintState CurrentState { get; private set; }
        public IReadOnlyCollection<SprintReviewDocument> ReviewDocuments => _reviewDocuments.AsReadOnly();

        public Sprint(Guid sprintId, string name, DateOnly startDate, DateOnly endDate, DomainEventPublisher eventPublisher)
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
            _eventPublisher = eventPublisher ?? throw new ArgumentNullException(nameof(eventPublisher));
            CurrentState = new ConceptSprintState();
        }

        public Sprint(Guid sprintId, string name, DomainEventPublisher eventPublisher)
            : this(
                sprintId,
                name,
                DateOnly.FromDateTime(DateTime.UtcNow),
                DateOnly.FromDateTime(DateTime.UtcNow.AddDays(14)),
                eventPublisher)
        {
        }

        public void MoveTo(SprintState state)
        {
            CurrentState = state ?? throw new ArgumentNullException(nameof(state));
        }

        public void moveTo(SprintState state) => MoveTo(state);

        public void Start() => CurrentState.Start(this);
        public void start() => Start();

        public void Finish() => CurrentState.Finish(this);
        public void finish() => Finish();

        public void StartRelease() => CurrentState.StartRelease(this);
        public void startRelease() => StartRelease();

        public void ReleaseSucceeded()
        {
            CurrentState.ReleaseSucceeded(this);

            if (CurrentState is ReleasedSprintState)
            {
                _eventPublisher.Publish(new SprintReleasedEvent(SprintId));
            }
        }

        public void releaseSucceeded() => ReleaseSucceeded();

        public void ReleaseFailed() => CurrentState.ReleaseFailed(this);
        public void releaseFailed() => ReleaseFailed();

        public void Close() => CurrentState.Close(this);
        public void close() => Close();

        // Backward-compatible convenience flow used by existing demo code.
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
