using Sofa3.Domain.Core;
using System;
using NUnit.Framework;

namespace TestProject1.Core.Test
{
    [TestFixture]
    public class MessageTests
    {
        [Test]
        public void Constructor_WithValidContent_ShouldCreateMessage()
        {
            var content = "Dit is een testbericht.";
            var message = new Message(content);

            Assert.Multiple(() =>
            {
                Assert.That(message.MessageId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(message.Content, Is.EqualTo(content));
                Assert.That(message.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            });
        }

        [Test]
        public void Constructor_ShouldSetCreatedAtToRecentUtcTime()
        {
            var beforeCreation = DateTime.UtcNow;
            var message = new Message("Testbericht");
            var afterCreation = DateTime.UtcNow;

            Assert.Multiple(() =>
            {
                Assert.That(message.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(message.CreatedAt, Is.LessThanOrEqualTo(afterCreation));
            });
        }

        [Test]
        public void Constructor_WithNullContent_ShouldSetContentToNull()
        {
            var message = new Message(null!);

            Assert.That(message.Content, Is.Null);
        }

        [Test]
        public void Constructor_WithEmptyContent_ShouldSetContentToEmptyString()
        {
            var message = new Message(string.Empty);

            Assert.That(message.Content, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Constructor_WithWhitespaceContent_ShouldPreserveWhitespace()
        {
            var message = new Message("   ");

            Assert.That(message.Content, Is.EqualTo("   "));
        }

        [Test]
        public void Constructor_EachMessage_ShouldHaveUniqueMessageId()
        {
            var firstMessage = new Message("Bericht 1");
            var secondMessage = new Message("Bericht 2");

            Assert.That(firstMessage.MessageId, Is.Not.EqualTo(secondMessage.MessageId));
        }
    }
}