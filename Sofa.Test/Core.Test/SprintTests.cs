using Sofa3.Domain.Core.BacklogItemStates;
using Sofa3.Domain.Core.SprintStates;
using Sofa3.Domain.Core;
using Sofa3.Domain.Notification.DomainEvents;
using System;
using System.Linq;
using NUnit.Framework;

namespace TestProject1.Core.Test
{
    [TestFixture]
    public class SprintTests
    {
        [Test]
        public void Constructor_WithValidData_ShouldCreateSprint()
        {
            var sprintId = Guid.NewGuid();
            var projectId = Guid.NewGuid();
            var name = "Sprint 1";
            var startDate = new DateOnly(2026, 4, 1);
            var endDate = new DateOnly(2026, 4, 14);

            var sprint = new Sprint(sprintId, projectId, name, startDate, endDate);

            Assert.Multiple(() =>
            {
                Assert.That(sprint.SprintId, Is.EqualTo(sprintId));
                Assert.That(sprint.ProjectId, Is.EqualTo(projectId));
                Assert.That(sprint.Name, Is.EqualTo(name));
                Assert.That(sprint.StartDate, Is.EqualTo(startDate));
                Assert.That(sprint.EndDate, Is.EqualTo(endDate));
                Assert.That(sprint.CurrentState, Is.TypeOf<ConceptSprintState>());
                Assert.That(sprint.BacklogItems, Is.Empty);
            });
        }

        [Test]
        public void Constructor_WithoutProjectId_ShouldSetProjectIdToEmptyGuid()
        {
            var sprintId = Guid.NewGuid();

            var sprint = new Sprint(
                sprintId,
                "Sprint 1",
                new DateOnly(2026, 4, 1),
                new DateOnly(2026, 4, 14));

            Assert.That(sprint.ProjectId, Is.EqualTo(Guid.Empty));
        }

        [Test]
        public void Constructor_WithNullName_ShouldThrowArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new Sprint(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    null!,
                    new DateOnly(2026, 4, 1),
                    new DateOnly(2026, 4, 14)));

            Assert.Multiple(() =>
            {
                Assert.That(ex!.Message, Does.Contain("Sprint name is required."));
                Assert.That(ex.ParamName, Is.EqualTo("name"));
            });
        }

        [Test]
        public void Constructor_WithWhitespaceName_ShouldThrowArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new Sprint(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    "   ",
                    new DateOnly(2026, 4, 1),
                    new DateOnly(2026, 4, 14)));

            Assert.Multiple(() =>
            {
                Assert.That(ex!.Message, Does.Contain("Sprint name is required."));
                Assert.That(ex.ParamName, Is.EqualTo("name"));
            });
        }

        [Test]
        public void Constructor_WithEndDateBeforeStartDate_ShouldThrowArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new Sprint(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    "Sprint 1",
                    new DateOnly(2026, 4, 14),
                    new DateOnly(2026, 4, 1)));

            Assert.Multiple(() =>
            {
                Assert.That(ex!.Message, Does.Contain("End date cannot be before start date."));
                Assert.That(ex.ParamName, Is.EqualTo("endDate"));
            });
        }

        [Test]
        public void Constructor_WithOnlySprintIdAndName_ShouldCreateSprintWithDefaultDates()
        {
            var sprintId = Guid.NewGuid();

            var sprint = new Sprint(sprintId, "Sprint 1");

            Assert.Multiple(() =>
            {
                Assert.That(sprint.SprintId, Is.EqualTo(sprintId));
                Assert.That(sprint.Name, Is.EqualTo("Sprint 1"));
                Assert.That(sprint.CurrentState, Is.TypeOf<ConceptSprintState>());
                Assert.That(sprint.EndDate, Is.GreaterThanOrEqualTo(sprint.StartDate));
            });
        }

        [Test]
        public void Constructor_WithProjectIdAndName_ShouldCreateSprintWithDefaultDates()
        {
            var sprintId = Guid.NewGuid();
            var projectId = Guid.NewGuid();

            var sprint = new Sprint(sprintId, projectId, "Sprint 1");

            Assert.Multiple(() =>
            {
                Assert.That(sprint.SprintId, Is.EqualTo(sprintId));
                Assert.That(sprint.ProjectId, Is.EqualTo(projectId));
                Assert.That(sprint.Name, Is.EqualTo("Sprint 1"));
                Assert.That(sprint.CurrentState, Is.TypeOf<ConceptSprintState>());
                Assert.That(sprint.EndDate, Is.GreaterThanOrEqualTo(sprint.StartDate));
            });
        }

        [Test]
        public void MoveTo_WithValidState_ShouldChangeCurrentState()
        {
            var sprint = CreateSprint();
            var newState = new ActiveSprintState();

            sprint.MoveTo(newState);

            Assert.That(sprint.CurrentState, Is.SameAs(newState));
        }

        [Test]
        public void MoveTo_WithNullState_ShouldThrowArgumentNullException()
        {
            var sprint = CreateSprint();

            var ex = Assert.Throws<ArgumentNullException>(() => sprint.MoveTo(null!));

            Assert.That(ex!.ParamName, Is.EqualTo("state"));
        }

        [Test]
        public void Release_WhenInConceptState_ShouldMoveToReleasedState_AndAddDomainEvent()
        {
            var sprint = CreateSprint();

            sprint.Release();

            Assert.Multiple(() =>
            {
                Assert.That(sprint.CurrentState, Is.TypeOf<ReleasedSprintState>());
                Assert.That(
                    sprint.DomainEvents.OfType<SprintReleasedEvent>().Any(e => e.SprintId == sprint.SprintId),
                    Is.True);
            });
        }

        [Test]
        public void Release_WhenAlreadyActive_ShouldMoveToReleasedState_AndAddDomainEvent()
        {
            var sprint = CreateSprint();
            sprint.MoveTo(new ActiveSprintState());

            sprint.Release();

            Assert.Multiple(() =>
            {
                Assert.That(sprint.CurrentState, Is.TypeOf<ReleasedSprintState>());
                Assert.That(
                    sprint.DomainEvents.OfType<SprintReleasedEvent>().Any(e => e.SprintId == sprint.SprintId),
                    Is.True);
            });
        }

        [Test]
        public void Release_WhenFinished_ShouldMoveToReleasedState_AndAddDomainEvent()
        {
            var sprint = CreateSprint();
            sprint.MoveTo(new FinishedSprintState());

            sprint.Release();

            Assert.Multiple(() =>
            {
                Assert.That(sprint.CurrentState, Is.TypeOf<ReleasedSprintState>());
                Assert.That(
                    sprint.DomainEvents.OfType<SprintReleasedEvent>().Any(e => e.SprintId == sprint.SprintId),
                    Is.True);
            });
        }

        [Test]
        public void Release_WhenFailed_ShouldMoveToReleasedState_AndAddDomainEvent()
        {
            var sprint = CreateSprint();
            sprint.MoveTo(new FailedSprintState());

            sprint.Release();

            Assert.Multiple(() =>
            {
                Assert.That(sprint.CurrentState, Is.TypeOf<ReleasedSprintState>());
                Assert.That(
                    sprint.DomainEvents.OfType<SprintReleasedEvent>().Any(e => e.SprintId == sprint.SprintId),
                    Is.True);
            });
        }

        [Test]
        public void AddReviewDocument_WithFileName_ShouldAddAndReturnDocument()
        {
            var sprint = CreateSprint();
            var fileName = "review.pdf";
            var uploadedAt = new DateTime(2026, 4, 3, 12, 0, 0, DateTimeKind.Utc);

            var document = sprint.AddReviewDocument(fileName, uploadedAt);

            Assert.Multiple(() =>
            {
                Assert.That(document, Is.Not.Null);
                Assert.That(document.FileName, Is.EqualTo(fileName));
                Assert.That(document.UploadedAt, Is.EqualTo(uploadedAt));
            });
        }

        [Test]
        public void AddReviewDocument_WithoutUploadedAt_ShouldUseCurrentUtcTime()
        {
            var sprint = CreateSprint();
            var before = DateTime.UtcNow;

            var document = sprint.AddReviewDocument("review.pdf");

            var after = DateTime.UtcNow;

            Assert.Multiple(() =>
            {
                Assert.That(document.FileName, Is.EqualTo("review.pdf"));
                Assert.That(document.UploadedAt, Is.InRange(before, after));
            });
        }

        [Test]
        public void AddBacklogItem_WithNullItem_ShouldThrowArgumentNullException()
        {
            var sprint = CreateSprint();

            var ex = Assert.Throws<ArgumentNullException>(() => sprint.AddBacklogItem(null!));

            Assert.That(ex!.ParamName, Is.EqualTo("item"));
        }

        [Test]
        public void AddBacklogItem_WhenSprintHasNoProjectId_ShouldThrowInvalidOperationException()
        {
            var sprint = new Sprint(
                Guid.NewGuid(),
                "Sprint 1",
                new DateOnly(2026, 4, 1),
                new DateOnly(2026, 4, 14));

            var item = CreateBacklogItem(Guid.NewGuid());

            var ex = Assert.Throws<InvalidOperationException>(() => sprint.AddBacklogItem(item));

            Assert.That(ex!.Message, Does.Contain("Sprint must be linked to a project before backlog items can be added."));
        }

        [Test]
        public void AddBacklogItem_WithDifferentProjectId_ShouldThrowInvalidOperationException()
        {
            var sprintProjectId = Guid.NewGuid();
            var itemProjectId = Guid.NewGuid();

            var sprint = CreateSprint(projectId: sprintProjectId);
            var item = CreateBacklogItem(itemProjectId);

            var ex = Assert.Throws<InvalidOperationException>(() => sprint.AddBacklogItem(item));

            Assert.That(ex!.Message, Does.Contain("Backlog item belongs to a different project."));
        }

        [Test]
        public void AddBacklogItem_WithMatchingProjectId_ShouldAddItemToSprint()
        {
            var projectId = Guid.NewGuid();
            var sprint = CreateSprint(projectId: projectId);
            var item = CreateBacklogItem(projectId);

            sprint.AddBacklogItem(item);

            Assert.Multiple(() =>
            {
                Assert.That(sprint.BacklogItems.Count, Is.EqualTo(1));
                Assert.That(sprint.BacklogItems.Contains(item), Is.True);
            });
        }

        [Test]
        public void AddBacklogItem_WhenSameItemAddedTwice_ShouldOnlyAddOnce()
        {
            var projectId = Guid.NewGuid();
            var sprint = CreateSprint(projectId: projectId);
            var item = CreateBacklogItem(projectId);

            sprint.AddBacklogItem(item);
            sprint.AddBacklogItem(item);

            Assert.That(sprint.BacklogItems.Count, Is.EqualTo(1));
        }

        private static Sprint CreateSprint(Guid? sprintId = null, Guid? projectId = null)
        {
            return new Sprint(
                sprintId ?? Guid.NewGuid(),
                projectId ?? Guid.NewGuid(),
                "Sprint 1",
                new DateOnly(2026, 4, 1),
                new DateOnly(2026, 4, 14));
        }

        private static BacklogItem CreateBacklogItem(Guid projectId)
        {
            return new BacklogItem(
                projectId,
                "Backlog Item 1",
                "Beschrijving",
                0,
                new ToDoState());
        }
    }
}