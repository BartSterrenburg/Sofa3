using Sofa3.Domain.Core;
using System;
using NUnit.Framework;

namespace TestProject1.Core.Test
{
    [TestFixture]
    public class ActivityTests
    {
        [Test]
        public void Constructor_WithValidTitleAndDescription_ShouldCreateActivity()
        {
            var title = "Implement login";
            var description = "Create login activity for sprint backlog";

            var activity = new Activity(title, description);

            Assert.Multiple(() =>
            {
                Assert.That(activity.ActivityId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(activity.Title, Is.EqualTo(title));
                Assert.That(activity.Description, Is.EqualTo(description));
                Assert.That(activity.Status, Is.EqualTo(ActivityStatus.TODO));
                Assert.That(activity.BacklogItemId, Is.Null);
            });
        }

        [Test]
        public void Constructor_WithNullDescription_ShouldSetEmptyString()
        {
            var title = "Implement logout";

            var activity = new Activity(title, null);

            Assert.Multiple(() =>
            {
                Assert.That(activity.Description, Is.EqualTo(string.Empty));
            });
        }

        [Test]
        public void Constructor_WithEmptyTitle_ShouldThrowArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new Activity("", "Some description"));

            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex!.Message, Does.Contain("Activity title is required."));
                Assert.That(ex.ParamName, Is.EqualTo("title"));
            });
        }

        [Test]
        public void Constructor_WithWhitespaceTitle_ShouldThrowArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new Activity("   ", "Some description"));

            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex!.Message, Does.Contain("Activity title is required."));
                Assert.That(ex.ParamName, Is.EqualTo("title"));
            });
        }

        [Test]
        public void MarkDone_ShouldSetStatusToDone()
        {
            var activity = new Activity("Test activity", "Test description");

            activity.MarkDone();

            Assert.Multiple(() =>
            {
                Assert.That(activity.Status, Is.EqualTo(ActivityStatus.DONE));
            });
        }

        [Test]
        public void LinkToBacklogItem_FirstTime_ShouldSetBacklogItemId()
        {
            var activity = new TestableActivity("Test activity", "Test description");
            var backlogItemId = Guid.NewGuid();

            activity.CallLinkToBacklogItem(backlogItemId);

            Assert.Multiple(() =>
            {
                Assert.That(activity.BacklogItemId, Is.EqualTo(backlogItemId));
            });
        }

        [Test]
        public void LinkToBacklogItem_WithSameBacklogItemIdAgain_ShouldNotThrow()
        {
            var activity = new TestableActivity("Test activity", "Test description");
            var backlogItemId = Guid.NewGuid();

            activity.CallLinkToBacklogItem(backlogItemId);

            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => activity.CallLinkToBacklogItem(backlogItemId));
                Assert.That(activity.BacklogItemId, Is.EqualTo(backlogItemId));
            });
        }

        private sealed class TestableActivity : Activity
        {
            public TestableActivity(string title, string description)
                : base(title, description)
            {
            }

            public void CallLinkToBacklogItem(Guid backlogItemId)
            {
                typeof(Activity)
                    .GetMethod("LinkToBacklogItem", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!
                    .Invoke(this, new object[] { backlogItemId });
            }
        }
    }
}