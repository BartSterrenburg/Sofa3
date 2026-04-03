using Sofa3.Domain.Core;
using Sofa3.Domain.Core.BacklogItemStates;
using System;
using NUnit.Framework;

namespace TestProject1.Core.Test
{
    [TestFixture]
    public class BacklogTests
    {
        [Test]
        public void Constructor_WithValidValues_ShouldCreateBacklog()
        {
            var backlogId = Guid.NewGuid();
            var projectId = Guid.NewGuid();
            var name = "Product Backlog";

            var backlog = new Backlog(backlogId, projectId, name);

            Assert.Multiple(() =>
            {
                Assert.That(backlog.BacklogId, Is.EqualTo(backlogId));
                Assert.That(backlog.ProjectId, Is.EqualTo(projectId));
                Assert.That(backlog.Name, Is.EqualTo(name));
                Assert.That(backlog.Items, Is.Not.Null);
                Assert.That(backlog.Items.Count, Is.EqualTo(0));
            });
        }

        [Test]
        public void Constructor_WithEmptyName_ShouldThrowArgumentException()
        {
            var backlogId = Guid.NewGuid();
            var projectId = Guid.NewGuid();

            var ex = Assert.Throws<ArgumentException>(() =>
                new Backlog(backlogId, projectId, string.Empty));

            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex!.Message, Does.Contain("Backlog name is required."));
                Assert.That(ex.ParamName, Is.EqualTo("name"));
            });
        }

        [Test]
        public void Constructor_WithWhitespaceName_ShouldThrowArgumentException()
        {
            var backlogId = Guid.NewGuid();
            var projectId = Guid.NewGuid();

            var ex = Assert.Throws<ArgumentException>(() =>
                new Backlog(backlogId, projectId, "   "));

            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex!.Message, Does.Contain("Backlog name is required."));
                Assert.That(ex.ParamName, Is.EqualTo("name"));
            });
        }

        [Test]
        public void AddItem_WithNull_ShouldThrowArgumentNullException()
        {
            var backlog = new Backlog(Guid.NewGuid(), Guid.NewGuid(), "Backlog");

            var ex = Assert.Throws<ArgumentNullException>(() => backlog.AddItem(null!));

            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex!.ParamName, Is.EqualTo("item"));
            });
        }

        [Test]
        public void AddItem_WithMatchingProjectId_ShouldAddItem()
        {
            var projectId = Guid.NewGuid();
            var backlog = new Backlog(Guid.NewGuid(), projectId, "Backlog");
            var item = CreateBacklogItem(projectId);

            backlog.AddItem(item);

            Assert.Multiple(() =>
            {
                Assert.That(backlog.Items.Count, Is.EqualTo(1));
                Assert.That(backlog.Items, Contains.Item(item));
            });
        }

        [Test]
        public void AddItem_WithDifferentProjectId_ShouldThrowInvalidOperationException()
        {
            var backlog = new Backlog(Guid.NewGuid(), Guid.NewGuid(), "Backlog");
            var otherProjectId = Guid.NewGuid();
            var item = CreateBacklogItem(otherProjectId);

            var ex = Assert.Throws<InvalidOperationException>(() => backlog.AddItem(item));

            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex!.Message, Is.EqualTo("Backlog item belongs to a different project."));
            });
        }

        [Test]
        public void RemoveItem_WithExistingItemId_ShouldRemoveItem()
        {
            var projectId = Guid.NewGuid();
            var backlog = new Backlog(Guid.NewGuid(), projectId, "Backlog");
            var item = CreateBacklogItem(projectId);

            backlog.AddItem(item);

            backlog.RemoveItem(item.BacklogItemId);

            Assert.Multiple(() =>
            {
                Assert.That(backlog.Items.Count, Is.EqualTo(0));
                Assert.That(backlog.Items, Does.Not.Contain(item));
            });
        }

        [Test]
        public void RemoveItem_WithNonExistingItemId_ShouldDoNothing()
        {
            var projectId = Guid.NewGuid();
            var backlog = new Backlog(Guid.NewGuid(), projectId, "Backlog");
            var item = CreateBacklogItem(projectId);
            backlog.AddItem(item);

            var nonExistingId = Guid.NewGuid();

            backlog.RemoveItem(nonExistingId);

            Assert.Multiple(() =>
            {
                Assert.That(backlog.Items.Count, Is.EqualTo(1));
                Assert.That(backlog.Items, Contains.Item(item));
            });
        }

        private static BacklogItem CreateBacklogItem(Guid projectId)
        {
            return new BacklogItem(
                projectId,
                "Test item",
                "This is a test backlog item.",
                1,
                new DoingState()
            );
        }
    }
}