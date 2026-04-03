using Sofa3.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.Core.Test
{
    [TestFixture]
    public class MessageTests
    {
        [Test]
        public void Constructor_WithValidContent_ShouldCreateMessage()
        {
            // Arrange
            var content = "Dit is een testbericht.";

            // Act
            var message = new Message(content);

            // Assert
            Assert.That(message.MessageId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(message.Content, Is.EqualTo(content));
            Assert.That(message.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        }

        [Test]
        public void Constructor_ShouldSetCreatedAtToRecentUtcTime()
        {
            // Arrange
            var beforeCreation = DateTime.UtcNow;

            // Act
            var message = new Message("Testbericht");

            // Arrange
            var afterCreation = DateTime.UtcNow;

            // Assert
            Assert.That(message.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation));
            Assert.That(message.CreatedAt, Is.LessThanOrEqualTo(afterCreation));
        }

        [Test]
        public void Constructor_WithNullContent_ShouldSetContentToNull()
        {
            // Act
            var message = new Message(null!);

            // Assert
            Assert.That(message.Content, Is.Null);
        }

        [Test]
        public void Constructor_WithEmptyContent_ShouldSetContentToEmptyString()
        {
            // Act
            var message = new Message(string.Empty);

            // Assert
            Assert.That(message.Content, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Constructor_WithWhitespaceContent_ShouldPreserveWhitespace()
        {
            // Act
            var message = new Message("   ");

            // Assert
            Assert.That(message.Content, Is.EqualTo("   "));
        }

        [Test]
        public void Constructor_EachMessage_ShouldHaveUniqueMessageId()
        {
            // Act
            var firstMessage = new Message("Bericht 1");
            var secondMessage = new Message("Bericht 2");

            // Assert
            Assert.That(firstMessage.MessageId, Is.Not.EqualTo(secondMessage.MessageId));
        }
    }
}