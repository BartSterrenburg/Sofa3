using Sofa3.Domain.Core;
using Sofa3.Domain.Notification;
using System;
using NUnit.Framework;

namespace TestProject1.Core.Test
{
    [TestFixture]
    public class AggregateRootTests
    {
        [Test]
        public void DomainEvents_Initially_ShouldBeEmpty()
        {
            var aggregate = new TestAggregateRoot();

            Assert.Multiple(() =>
            {
                Assert.That(aggregate.DomainEvents, Is.Not.Null);
                Assert.That(aggregate.DomainEvents, Has.Count.EqualTo(0));
            });
        }

        [Test]
        public void AddDomainEvent_WithValidEvent_ShouldAddEventToCollection()
        {
            var aggregate = new TestAggregateRoot();
            var domainEvent = new TestDomainEvent();

            aggregate.AddEvent(domainEvent);

            Assert.Multiple(() =>
            {
                Assert.That(aggregate.DomainEvents, Has.Count.EqualTo(1));
                Assert.That(aggregate.DomainEvents, Does.Contain(domainEvent));
            });
        }

        [Test]
        public void AddDomainEvent_WithNull_ShouldThrowArgumentNullException()
        {
            var aggregate = new TestAggregateRoot();

            var ex = Assert.Throws<ArgumentNullException>(() => aggregate.AddEvent(null!));

            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
            });
        }

        [Test]
        public void ClearDomainEvents_AfterAddingEvents_ShouldRemoveAllEvents()
        {
            var aggregate = new TestAggregateRoot();
            aggregate.AddEvent(new TestDomainEvent());
            aggregate.AddEvent(new TestDomainEvent());

            aggregate.ClearDomainEvents();

            Assert.Multiple(() =>
            {
                Assert.That(aggregate.DomainEvents, Has.Count.EqualTo(0));
            });
        }

        [Test]
        public void DomainEvents_ShouldExposeReadOnlyCollection()
        {
            var aggregate = new TestAggregateRoot();
            aggregate.AddEvent(new TestDomainEvent());

            Assert.Multiple(() =>
            {
                Assert.That(aggregate.DomainEvents, Is.AssignableTo<IReadOnlyCollection<DomainEvent>>());
            });
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