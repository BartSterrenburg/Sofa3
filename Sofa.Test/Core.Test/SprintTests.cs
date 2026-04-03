using Sofa3.Domain.Core.BacklogItemStates;
using Sofa3.Domain.Core.SprintStates;
using Sofa3.Domain.Core;
using Sofa3.Domain.Notification.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.Core.Test
{
    [TestFixture]
    public class SprintTests
    {
        [Test]
        public void Constructor_WithValidData_ShouldCreateSprint()
        {
            // Arrange
            var sprintId = Guid.NewGuid();
            var projectId = Guid.NewGuid();
            var name = "Sprint 1";
            var startDate = new DateOnly(2026, 4, 1);
            var endDate = new DateOnly(2026, 4, 14);

            // Act
            var sprint = new Sprint(sprintId, projectId, name, startDate, endDate);

            // Assert
            Assert.That(sprint.SprintId, Is.EqualTo(sprintId));
            Assert.That(sprint.ProjectId, Is.EqualTo(projectId));
            Assert.That(sprint.Name, Is.EqualTo(name));
            Assert.That(sprint.StartDate, Is.EqualTo(startDate));
            Assert.That(sprint.EndDate, Is.EqualTo(endDate));
            Assert.That(sprint.CurrentState, Is.TypeOf<ConceptSprintState>());
            Assert.That(sprint.BacklogItems, Is.Empty);
        }

        [Test]
        public void Constructor_WithoutProjectId_ShouldSetProjectIdToEmptyGuid()
        {
            // Arrange
            var sprintId = Guid.NewGuid();

            // Act
            var sprint = new Sprint(
                sprintId,
                "Sprint 1",
                new DateOnly(2026, 4, 1),
                new DateOnly(2026, 4, 14));

            // Assert
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

            Assert.That(ex!.Message, Does.Contain("Sprint name is required."));
            Assert.That(ex.ParamName, Is.EqualTo("name"));
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

            Assert.That(ex!.Message, Does.Contain("Sprint name is required."));
            Assert.That(ex.ParamName, Is.EqualTo("name"));
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

            Assert.That(ex!.Message, Does.Contain("End date cannot be before start date."));
            Assert.That(ex.ParamName, Is.EqualTo("endDate"));
        }

        [Test]
        public void Constructor_WithOnlySprintIdAndName_ShouldCreateSprintWithDefaultDates()
        {
            // Arrange
            var sprintId = Guid.NewGuid();

            // Act
            var sprint = new Sprint(sprintId, "Sprint 1");

            // Assert
            Assert.That(sprint.SprintId, Is.EqualTo(sprintId));
            Assert.That(sprint.Name, Is.EqualTo("Sprint 1"));
            Assert.That(sprint.CurrentState, Is.TypeOf<ConceptSprintState>());
            Assert.That(sprint.EndDate, Is.GreaterThanOrEqualTo(sprint.StartDate));
        }

        [Test]
        public void Constructor_WithProjectIdAndName_ShouldCreateSprintWithDefaultDates()
        {
            // Arrange
            var sprintId = Guid.NewGuid();
            var projectId = Guid.NewGuid();

            // Act
            var sprint = new Sprint(sprintId, projectId, "Sprint 1");

            // Assert
            Assert.That(sprint.SprintId, Is.EqualTo(sprintId));
            Assert.That(sprint.ProjectId, Is.EqualTo(projectId));
            Assert.That(sprint.Name, Is.EqualTo("Sprint 1"));
            Assert.That(sprint.CurrentState, Is.TypeOf<ConceptSprintState>());
            Assert.That(sprint.EndDate, Is.GreaterThanOrEqualTo(sprint.StartDate));
        }

        [Test]
        public void MoveTo_WithValidState_ShouldChangeCurrentState()
        {
            // Arrange
            var sprint = CreateSprint();
            var newState = new ActiveSprintState();

            // Act
            sprint.MoveTo(newState);

            // Assert
            Assert.That(sprint.CurrentState, Is.SameAs(newState));
        }

        [Test]
        public void MoveTo_WithNullState_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sprint = CreateSprint();

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => sprint.MoveTo(null!));
            Assert.That(ex!.ParamName, Is.EqualTo("state"));
        }

        [Test]
        public void Release_WhenInConceptState_ShouldMoveToReleasedState_AndAddDomainEvent()
        {
            // Arrange
            var sprint = CreateSprint();

            // Act
            sprint.Release();

            // Assert
            Assert.That(sprint.CurrentState, Is.TypeOf<ReleasedSprintState>());
            Assert.That(
                sprint.DomainEvents.OfType<SprintReleasedEvent>().Any(e => e.SprintId == sprint.SprintId),
                Is.True);
        }

        [Test]
        public void Release_WhenAlreadyActive_ShouldMoveToReleasedState_AndAddDomainEvent()
        {
            // Arrange
            var sprint = CreateSprint();
            sprint.MoveTo(new ActiveSprintState());

            // Act
            sprint.Release();

            // Assert
            Assert.That(sprint.CurrentState, Is.TypeOf<ReleasedSprintState>());
            Assert.That(
                sprint.DomainEvents.OfType<SprintReleasedEvent>().Any(e => e.SprintId == sprint.SprintId),
                Is.True);
        }

        [Test]
        public void Release_WhenFinished_ShouldMoveToReleasedState_AndAddDomainEvent()
        {
            // Arrange
            var sprint = CreateSprint();
            sprint.MoveTo(new FinishedSprintState());

            // Act
            sprint.Release();

            // Assert
            Assert.That(sprint.CurrentState, Is.TypeOf<ReleasedSprintState>());
            Assert.That(
                sprint.DomainEvents.OfType<SprintReleasedEvent>().Any(e => e.SprintId == sprint.SprintId),
                Is.True);
        }

        [Test]
        public void Release_WhenFailed_ShouldMoveToReleasedState_AndAddDomainEvent()
        {
            // Arrange
            var sprint = CreateSprint();
            sprint.MoveTo(new FailedSprintState());

            // Act
            sprint.Release();

            // Assert
            Assert.That(sprint.CurrentState, Is.TypeOf<ReleasedSprintState>());
            Assert.That(
                sprint.DomainEvents.OfType<SprintReleasedEvent>().Any(e => e.SprintId == sprint.SprintId),
                Is.True);
        }

        [Test]
        public void AddReviewDocument_WithFileName_ShouldAddAndReturnDocument()
        {
            // Arrange
            var sprint = CreateSprint();
            var fileName = "review.pdf";
            var uploadedAt = new DateTime(2026, 4, 3, 12, 0, 0, DateTimeKind.Utc);

            // Act
            var document = sprint.AddReviewDocument(fileName, uploadedAt);

            // Assert
            Assert.That(document, Is.Not.Null);
            Assert.That(document.FileName, Is.EqualTo(fileName));
            Assert.That(document.UploadedAt, Is.EqualTo(uploadedAt));
        }

        [Test]
        public void AddReviewDocument_WithoutUploadedAt_ShouldUseCurrentUtcTime()
        {
            // Arrange
            var sprint = CreateSprint();
            var before = DateTime.UtcNow;

            // Act
            var document = sprint.AddReviewDocument("review.pdf");

            // Assert
            var after = DateTime.UtcNow;

            Assert.That(document.FileName, Is.EqualTo("review.pdf"));
            Assert.That(document.UploadedAt, Is.InRange(before, after));
        }

        [Test]
        public void AddBacklogItem_WithNullItem_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sprint = CreateSprint();

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => sprint.AddBacklogItem(null!));
            Assert.That(ex!.ParamName, Is.EqualTo("item"));
        }

        [Test]
        public void AddBacklogItem_WhenSprintHasNoProjectId_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var sprint = new Sprint(
                Guid.NewGuid(),
                "Sprint 1",
                new DateOnly(2026, 4, 1),
                new DateOnly(2026, 4, 14));

            var item = CreateBacklogItem(Guid.NewGuid());

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => sprint.AddBacklogItem(item));
            Assert.That(ex!.Message, Does.Contain("Sprint must be linked to a project before backlog items can be added."));
        }

        [Test]
        public void AddBacklogItem_WithDifferentProjectId_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var sprintProjectId = Guid.NewGuid();
            var itemProjectId = Guid.NewGuid();

            var sprint = CreateSprint(projectId: sprintProjectId);
            var item = CreateBacklogItem(itemProjectId);

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => sprint.AddBacklogItem(item));
            Assert.That(ex!.Message, Does.Contain("Backlog item belongs to a different project."));
        }

        [Test]
        public void AddBacklogItem_WithMatchingProjectId_ShouldAddItemToSprint()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var sprint = CreateSprint(projectId: projectId);
            var item = CreateBacklogItem(projectId);

            // Act
            sprint.AddBacklogItem(item);

            // Assert
            Assert.That(sprint.BacklogItems.Count, Is.EqualTo(1));
            Assert.That(sprint.BacklogItems.Contains(item), Is.True);
        }

        [Test]
        public void AddBacklogItem_WhenSameItemAddedTwice_ShouldOnlyAddOnce()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var sprint = CreateSprint(projectId: projectId);
            var item = CreateBacklogItem(projectId);

            // Act
            sprint.AddBacklogItem(item);
            sprint.AddBacklogItem(item);

            // Assert
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