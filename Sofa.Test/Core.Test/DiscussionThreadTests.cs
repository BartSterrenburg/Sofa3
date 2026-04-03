using Sofa3.Domain.Core;
using Sofa3.Domain.Notification.DomainEvents;
using System;
using System.Linq;
using NUnit.Framework;

namespace TestProject1.Core.Test
{
    [TestFixture]
    public class DiscussionThreadTests
    {
        [Test]
        public void Constructor_WithValidSubject_ShouldCreateDiscussionThread()
        {
            var subject = "API design discussie";

            var thread = new DiscussionThread(subject);

            Assert.Multiple(() =>
            {
                Assert.That(thread.ThreadId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(thread.Subject, Is.EqualTo(subject));
                Assert.That(thread.IsClosed, Is.False);
                Assert.That(thread.Messages, Is.Not.Null);
                Assert.That(thread.Messages, Has.Count.EqualTo(0));
                Assert.That(thread.DomainEvents, Has.Count.EqualTo(0));
            });
        }

        [Test]
        public void AddMessage_WithValidMessage_ShouldAddMessage()
        {
            var thread = new DiscussionThread("Architectuur");
            var message = CreateMessage("We moeten REST gebruiken.");

            thread.AddMessage(message);

            Assert.Multiple(() =>
            {
                Assert.That(thread.Messages, Has.Count.EqualTo(1));
                Assert.That(thread.Messages.First(), Is.EqualTo(message));
            });
        }

        [Test]
        public void AddMessage_WithValidMessage_ShouldAddDomainEvent()
        {
            var thread = new DiscussionThread("Architectuur");
            var message = CreateMessage("We moeten REST gebruiken.");

            thread.AddMessage(message);

            Assert.Multiple(() =>
            {
                Assert.That(thread.DomainEvents, Has.Count.EqualTo(1));
                Assert.That(thread.DomainEvents.First(), Is.TypeOf<DiscussionMessageAddedEvent>());
            });
        }

        [Test]
        public void AddMessage_WithNull_ShouldThrowArgumentNullException()
        {
            var thread = new DiscussionThread("Architectuur");

            var ex = Assert.Throws<ArgumentNullException>(() => thread.AddMessage(null!));

            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex!.ParamName, Is.EqualTo("message"));
            });
        }

        [Test]
        public void AddMessage_WhenThreadIsClosed_ShouldThrowInvalidOperationException()
        {
            var thread = new DiscussionThread("Architectuur");
            var message = CreateMessage("Nieuwe reactie");

            thread.CloseThread();

            var ex = Assert.Throws<InvalidOperationException>(() => thread.AddMessage(message));

            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex!.Message, Is.EqualTo("Thread is gesloten."));
            });
        }

        [Test]
        public void CloseThread_ShouldSetIsClosedToTrue()
        {
            var thread = new DiscussionThread("Architectuur");

            thread.CloseThread();

            Assert.Multiple(() =>
            {
                Assert.That(thread.IsClosed, Is.True);
            });
        }

        [Test]
        public void ReopenThread_AfterClosing_ShouldSetIsClosedToFalse()
        {
            var thread = new DiscussionThread("Architectuur");
            thread.CloseThread();

            thread.ReopenThread();

            Assert.Multiple(() =>
            {
                Assert.That(thread.IsClosed, Is.False);
            });
        }

        [Test]
        public void ReopenThread_AfterClosing_ShouldAllowAddingMessagesAgain()
        {
            var thread = new DiscussionThread("Architectuur");
            thread.CloseThread();
            thread.ReopenThread();

            var message = CreateMessage("Thread is weer open.");

            thread.AddMessage(message);

            Assert.Multiple(() =>
            {
                Assert.That(thread.Messages, Has.Count.EqualTo(1));
                Assert.That(thread.Messages.First(), Is.EqualTo(message));
            });
        }

        private static Message CreateMessage(string content)
        {
            return new Message(content);
        }
    }
}