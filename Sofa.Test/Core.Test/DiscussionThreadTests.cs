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
    public class DiscussionThreadTests
    {
        [Test]
        public void Constructor_WithValidSubject_ShouldCreateDiscussionThread()
        {
            // Arrange
            var subject = "API design discussie";

            // Act
            var thread = new DiscussionThread(subject);

            // Assert
            Assert.That(thread.ThreadId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(thread.Subject, Is.EqualTo(subject));
            Assert.That(thread.IsClosed, Is.False);
            Assert.That(thread.Messages, Is.Not.Null);
            Assert.That(thread.Messages.Count, Is.EqualTo(0));
            Assert.That(thread.DomainEvents.Count, Is.EqualTo(0));
        }

        [Test]
        public void AddMessage_WithValidMessage_ShouldAddMessage()
        {
            // Arrange
            var thread = new DiscussionThread("Architectuur");
            var message = CreateMessage("We moeten REST gebruiken.");

            // Act
            thread.AddMessage(message);

            // Assert
            Assert.That(thread.Messages.Count, Is.EqualTo(1));
            Assert.That(thread.Messages.First(), Is.EqualTo(message));
        }

        [Test]
        public void AddMessage_WithValidMessage_ShouldAddDomainEvent()
        {
            // Arrange
            var thread = new DiscussionThread("Architectuur");
            var message = CreateMessage("We moeten REST gebruiken.");

            // Act
            thread.AddMessage(message);

            // Assert
            Assert.That(thread.DomainEvents.Count, Is.EqualTo(1));
            Assert.That(thread.DomainEvents.First(), Is.TypeOf<DiscussionMessageAddedEvent>());
        }

        [Test]
        public void AddMessage_WithNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var thread = new DiscussionThread("Architectuur");

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => thread.AddMessage(null!));

            // Assert
            Assert.That(ex!.ParamName, Is.EqualTo("message"));
        }

        [Test]
        public void AddMessage_WhenThreadIsClosed_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var thread = new DiscussionThread("Architectuur");
            var message = CreateMessage("Nieuwe reactie");

            thread.CloseThread();

            // Act
            var ex = Assert.Throws<InvalidOperationException>(() => thread.AddMessage(message));

            // Assert
            Assert.That(ex!.Message, Is.EqualTo("Thread is gesloten."));
        }

        [Test]
        public void CloseThread_ShouldSetIsClosedToTrue()
        {
            // Arrange
            var thread = new DiscussionThread("Architectuur");

            // Act
            thread.CloseThread();

            // Assert
            Assert.That(thread.IsClosed, Is.True);
        }

        [Test]
        public void ReopenThread_AfterClosing_ShouldSetIsClosedToFalse()
        {
            // Arrange
            var thread = new DiscussionThread("Architectuur");
            thread.CloseThread();

            // Act
            thread.ReopenThread();

            // Assert
            Assert.That(thread.IsClosed, Is.False);
        }

        [Test]
        public void ReopenThread_AfterClosing_ShouldAllowAddingMessagesAgain()
        {
            // Arrange
            var thread = new DiscussionThread("Architectuur");
            thread.CloseThread();
            thread.ReopenThread();

            var message = CreateMessage("Thread is weer open.");

            // Act
            thread.AddMessage(message);

            // Assert
            Assert.That(thread.Messages.Count, Is.EqualTo(1));
            Assert.That(thread.Messages.First(), Is.EqualTo(message));
        }

        private static Message CreateMessage(string content)
        {
            return new Message(content);
        }
    }
}
