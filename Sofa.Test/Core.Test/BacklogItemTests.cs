using Sofa3.Domain.Core.BacklogItemStates;
using Sofa3.Domain.Core;
using Sofa3.Domain.Notification.DomainEvents;
using System;
using System.Linq;
using NUnit.Framework;

namespace TestProject1.Core.Test
{
    [TestFixture]
    public class BacklogItemTests
    {
        [Test]
        public void Constructor_WithValidValues_ShouldCreateBacklogItem()
        {
            var projectId = Guid.NewGuid();
            var initialState = CreateState();

            var item = new BacklogItem(projectId, "Login feature", "Build login", 5, initialState);

            Assert.Multiple(() =>
            {
                Assert.That(item.BacklogItemId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(item.ProjectId, Is.EqualTo(projectId));
                Assert.That(item.SprintId, Is.Null);
                Assert.That(item.Title, Is.EqualTo("Login feature"));
                Assert.That(item.Description, Is.EqualTo("Build login"));
                Assert.That(item.StoryPoints, Is.EqualTo(5));
                Assert.That(item.IsLocked, Is.False);
                Assert.That(item.State, Is.EqualTo(initialState));
                Assert.That(item.Owner, Is.Null);
                Assert.That(item.Activities.Count, Is.EqualTo(0));
                Assert.That(item.DiscussionThreads.Count, Is.EqualTo(0));
            });
        }

        [Test]
        public void Constructor_WithEmptyTitle_ShouldThrowArgumentException()
        {
            var projectId = Guid.NewGuid();
            var initialState = CreateState();

            var ex = Assert.Throws<ArgumentException>(() =>
                new BacklogItem(projectId, "", "Description", 3, initialState));

            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex!.Message, Does.Contain("Backlog item title is required."));
                Assert.That(ex.ParamName, Is.EqualTo("title"));
            });
        }

        [Test]
        public void Constructor_WithWhitespaceTitle_ShouldThrowArgumentException()
        {
            var projectId = Guid.NewGuid();
            var initialState = CreateState();

            var ex = Assert.Throws<ArgumentException>(() =>
                new BacklogItem(projectId, "   ", "Description", 3, initialState));

            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex!.Message, Does.Contain("Backlog item title is required."));
                Assert.That(ex.ParamName, Is.EqualTo("title"));
            });
        }

        [Test]
        public void Constructor_WithNullInitialState_ShouldThrowArgumentNullException()
        {
            var projectId = Guid.NewGuid();

            var ex = Assert.Throws<ArgumentNullException>(() =>
                new BacklogItem(projectId, "Feature", "Description", 3, null!));

            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex!.ParamName, Is.EqualTo("initialState"));
            });
        }

        [Test]
        public void AssignDeveloper_ShouldSetOwner()
        {
            var item = CreateBacklogItem();
            var user = CreateUser();

            item.AssignDeveloper(user);

            Assert.Multiple(() =>
            {
                Assert.That(item.Owner, Is.EqualTo(user));
            });
        }

        [Test]
        public void AddActivity_WithNull_ShouldThrowArgumentNullException()
        {
            var item = CreateBacklogItem();

            var ex = Assert.Throws<ArgumentNullException>(() => item.AddActivity(null!));

            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex!.ParamName, Is.EqualTo("activity"));
            });
        }

        [Test]
        public void AddActivity_WithValidActivity_ShouldAddActivityAndLinkItToBacklogItem()
        {
            var item = CreateBacklogItem();
            var activity = new Activity("Implement API", "Create endpoint");

            item.AddActivity(activity);

            Assert.Multiple(() =>
            {
                Assert.That(item.Activities.Count, Is.EqualTo(1));
                Assert.That(item.Activities.First(), Is.EqualTo(activity));
                Assert.That(activity.BacklogItemId, Is.EqualTo(item.BacklogItemId));
            });
        }

        [Test]
        public void AddDiscussionThread_ShouldAddThread()
        {
            var item = CreateBacklogItem();

            item.AddDiscussionThread("API design discussion", new object());

            Assert.Multiple(() =>
            {
                Assert.That(item.DiscussionThreads.Count, Is.EqualTo(1));
                Assert.That(item.DiscussionThreads.First().Subject, Is.EqualTo("API design discussion"));
            });
        }

        [Test]
        public void LinkToSprint_FirstTime_ShouldSetSprintId()
        {
            var item = new TestableBacklogItem(Guid.NewGuid(), "Feature", "Description", 5, CreateState());
            var sprintId = Guid.NewGuid();

            item.CallLinkToSprint(sprintId);

            Assert.Multiple(() =>
            {
                Assert.That(item.SprintId, Is.EqualTo(sprintId));
            });
        }

        [Test]
        public void LinkToSprint_WithSameSprintIdAgain_ShouldNotThrow()
        {
            var item = new TestableBacklogItem(Guid.NewGuid(), "Feature", "Description", 5, CreateState());
            var sprintId = Guid.NewGuid();

            item.CallLinkToSprint(sprintId);

            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => item.CallLinkToSprint(sprintId));
                Assert.That(item.SprintId, Is.EqualTo(sprintId));
            });
        }

        [Test]
        public void LinkToSprint_WithDifferentSprintId_ShouldThrowInvalidOperationException()
        {
            var item = new TestableBacklogItem(Guid.NewGuid(), "Feature", "Description", 5, CreateState());
            var firstSprintId = Guid.NewGuid();
            var secondSprintId = Guid.NewGuid();

            item.CallLinkToSprint(firstSprintId);

            var ex = Assert.Throws<InvalidOperationException>(() =>
                item.CallLinkToSprint(secondSprintId));

            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex!.Message, Is.EqualTo("Backlog item already belongs to a different sprint."));
            });
        }

        [Test]
        public void MoveTo_WithNonToDoState_ShouldChangeStateWithoutAddingDomainEvent()
        {
            var item = CreateBacklogItem();
            var newState = CreateState();

            item.MoveTo(newState);

            Assert.Multiple(() =>
            {
                Assert.That(item.State, Is.EqualTo(newState));
                Assert.That(item.DomainEvents.Count, Is.EqualTo(0));
            });
        }

        [Test]
        public void MoveTo_WithToDoState_ShouldChangeStateAndAddDomainEvent()
        {
            var item = CreateBacklogItem();
            var toDoState = new ToDoState();

            item.MoveTo(toDoState);

            Assert.Multiple(() =>
            {
                Assert.That(item.State, Is.EqualTo(toDoState));
                Assert.That(item.DomainEvents.Count, Is.EqualTo(1));
                Assert.That(item.DomainEvents.First(), Is.TypeOf<BacklogItemReturnedToToDoEvent>());
            });
        }

        private static BacklogItem CreateBacklogItem()
        {
            return new BacklogItem(
                Guid.NewGuid(),
                "Test backlog item",
                "Test description",
                3,
                CreateState());
        }

        private static IBacklogItemState CreateState()
        {
            return new FakeBacklogItemState();
        }

        private static User CreateUser()
        {
            return new User(Guid.NewGuid(), "Testperson", "test@mail.com");
        }

        private sealed class TestableBacklogItem : BacklogItem
        {
            public TestableBacklogItem(Guid projectId, string title, string description, int storyPoints, IBacklogItemState initialState)
                : base(projectId, title, description, storyPoints, initialState)
            {
            }

            public void CallLinkToSprint(Guid sprintId)
            {
                try
                {
                    typeof(BacklogItem)
                        .GetMethod("LinkToSprint", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!
                        .Invoke(this, new object[] { sprintId });
                }
                catch (System.Reflection.TargetInvocationException ex) when (ex.InnerException is not null)
                {
                    throw ex.InnerException;
                }
            }
        }

        private sealed class FakeBacklogItemState : IBacklogItemState
        {
            public void MoveToDoing(BacklogItem item)
            {
                throw new NotImplementedException();
            }

            public void MoveToDone(BacklogItem item)
            {
                throw new NotImplementedException();
            }

            public void MoveToReadyForTesting(BacklogItem item)
            {
                throw new NotImplementedException();
            }

            public void MoveToTested(BacklogItem item)
            {
                throw new NotImplementedException();
            }

            public void MoveToTesting(BacklogItem item)
            {
                throw new NotImplementedException();
            }
        }
    }
}