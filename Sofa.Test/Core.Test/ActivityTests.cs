using Sofa3.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.Core.Test
{
    [TestFixture]
    public class ActivityTests
    {
        [Test]
        public void Constructor_WithValidTitleAndDescription_ShouldCreateActivity()
        {
            // Arrange
            var title = "Implement login";
            var description = "Create login activity for sprint backlog";

            // Act
            var activity = new Activity(title, description);

            // Assert
            Assert.That(activity.ActivityId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(activity.Title, Is.EqualTo(title));
            Assert.That(activity.Description, Is.EqualTo(description));
            Assert.That(activity.Status, Is.EqualTo(ActivityStatus.TODO));
            Assert.That(activity.BacklogItemId, Is.Null);
        }

        [Test]
        public void Constructor_WithNullDescription_ShouldSetEmptyString()
        {
            // Arrange
            var title = "Implement logout";

            // Act
            var activity = new Activity(title, null);

            // Assert
            Assert.That(activity.Description, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Constructor_WithEmptyTitle_ShouldThrowArgumentException()
        {
            // Act
            var ex = Assert.Throws<ArgumentException>(() => new Activity("", "Some description"));

            // Assert
            Assert.That(ex!.Message, Does.Contain("Activity title is required."));
            Assert.That(ex.ParamName, Is.EqualTo("title"));
        }

        [Test]
        public void Constructor_WithWhitespaceTitle_ShouldThrowArgumentException()
        {
            // Act
            var ex = Assert.Throws<ArgumentException>(() => new Activity("   ", "Some description"));

            // Assert
            Assert.That(ex!.Message, Does.Contain("Activity title is required."));
            Assert.That(ex.ParamName, Is.EqualTo("title"));
        }

        [Test]
        public void MarkDone_ShouldSetStatusToDone()
        {
            // Arrange
            var activity = new Activity("Test activity", "Test description");

            // Act
            activity.MarkDone();

            // Assert
            Assert.That(activity.Status, Is.EqualTo(ActivityStatus.DONE));
        }

        [Test]
        public void LinkToBacklogItem_FirstTime_ShouldSetBacklogItemId()
        {
            // Arrange
            var activity = new TestableActivity("Test activity", "Test description");
            var backlogItemId = Guid.NewGuid();

            // Act
            activity.CallLinkToBacklogItem(backlogItemId);

            // Assert
            Assert.That(activity.BacklogItemId, Is.EqualTo(backlogItemId));
        }

        [Test]
        public void LinkToBacklogItem_WithSameBacklogItemIdAgain_ShouldNotThrow()
        {
            // Arrange
            var activity = new TestableActivity("Test activity", "Test description");
            var backlogItemId = Guid.NewGuid();

            activity.CallLinkToBacklogItem(backlogItemId);

            // Act / Assert
            Assert.DoesNotThrow(() => activity.CallLinkToBacklogItem(backlogItemId));
            Assert.That(activity.BacklogItemId, Is.EqualTo(backlogItemId));
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