using Sofa3.Domain.Core;
using Sofa3.Domain.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.Core.Test
{
    [TestFixture]
    public class AggregateRootTests
    {
        [Test]
        public void DomainEvents_Initially_ShouldBeEmpty()
        {
            // Arrange
            var aggregate = new TestAggregateRoot();

            // Act & Assert
            Assert.That(aggregate.DomainEvents, Is.Not.Null);
            Assert.That(aggregate.DomainEvents.Count, Is.EqualTo(0));
        }

        [Test]
        public void AddDomainEvent_WithValidEvent_ShouldAddEventToCollection()
        {
            // Arrange
            var aggregate = new TestAggregateRoot();
            var domainEvent = new TestDomainEvent();

            // Act
            aggregate.AddEvent(domainEvent);

            // Assert
            Assert.That(aggregate.DomainEvents.Count, Is.EqualTo(1));
            Assert.That(aggregate.DomainEvents, Contains.Item(domainEvent));
        }

        [Test]
        public void AddDomainEvent_WithNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var aggregate = new TestAggregateRoot();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => aggregate.AddEvent(null!));
        }

        [Test]
        public void ClearDomainEvents_AfterAddingEvents_ShouldRemoveAllEvents()
        {
            // Arrange
            var aggregate = new TestAggregateRoot();
            aggregate.AddEvent(new TestDomainEvent());
            aggregate.AddEvent(new TestDomainEvent());

            // Act
            aggregate.ClearDomainEvents();

            // Assert
            Assert.That(aggregate.DomainEvents.Count, Is.EqualTo(0));
        }

        [Test]
        public void DomainEvents_ShouldExposeReadOnlyCollection()
        {
            // Arrange
            var aggregate = new TestAggregateRoot();
            aggregate.AddEvent(new TestDomainEvent());

            // Act & Assert
            Assert.That(aggregate.DomainEvents, Is.AssignableTo<System.Collections.Generic.IReadOnlyCollection<DomainEvent>>());
        }

        private sealed class TestAggregateRoot : AggregateRoot
        {
            public void AddEvent(DomainEvent domainEvent)
            {
                AddDomainEvent(domainEvent);
            }
        }

        private sealed class TestDomainEvent : DomainEvent
        {
        }
    }
}
