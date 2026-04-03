using Sofa3.Domain.Core.BacklogItemStates;
using Sofa3.Domain.Core;
using System;
using NUnit.Framework;

namespace TestProject1.Core.Test
{
    [TestFixture]
    public class ProjectTests
    {
        [Test]
        public void Constructor_WithValidData_ShouldCreateProject()
        {
            var name = "SOFA3";
            var description = "Project description";

            var project = new Project(name, description);

            Assert.Multiple(() =>
            {
                Assert.That(project.ProjectId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(project.Name, Is.EqualTo(name));
                Assert.That(project.Description, Is.EqualTo(description));
                Assert.That(project.CreatedAt, Is.LessThanOrEqualTo(DateTime.UtcNow));
                Assert.That(project.Backlog, Is.Not.Null);
                Assert.That(project.Backlog.Name, Is.EqualTo("Product Backlog"));
                Assert.That(project.Members, Is.Not.Null);
                Assert.That(project.Members, Is.Empty);
            });
        }

        [Test]
        public void Constructor_WithNullName_ShouldThrowArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new Project(null!, "Beschrijving"));

            Assert.Multiple(() =>
            {
                Assert.That(ex!.Message, Does.Contain("Project name is required."));
                Assert.That(ex.ParamName, Is.EqualTo("name"));
            });
        }

        [Test]
        public void Constructor_WithEmptyName_ShouldThrowArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new Project("", "Beschrijving"));

            Assert.Multiple(() =>
            {
                Assert.That(ex!.Message, Does.Contain("Project name is required."));
                Assert.That(ex.ParamName, Is.EqualTo("name"));
            });
        }

        [Test]
        public void Constructor_WithWhitespaceName_ShouldThrowArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new Project("   ", "Beschrijving"));

            Assert.Multiple(() =>
            {
                Assert.That(ex!.Message, Does.Contain("Project name is required."));
                Assert.That(ex.ParamName, Is.EqualTo("name"));
            });
        }

        [Test]
        public void Constructor_WithNullDescription_ShouldSetDescriptionToEmptyString()
        {
            var project = new Project("SOFA3", null!);

            Assert.That(project.Description, Is.EqualTo(string.Empty));
        }

        [Test]
        public void AddMember_WithValidUser_ShouldAddMembership()
        {
            var project = new Project("SOFA3", "Beschrijving");
            var user = new User(Guid.NewGuid(), "Bart", "bart@mail.com");
            var role = ProjectRole.DEVELOPER;

            project.AddMember(user, role);

            Assert.Multiple(() =>
            {
                Assert.That(project.Members.Count, Is.EqualTo(1));
                Assert.That(project.Members[0].UserId, Is.EqualTo(user.UserId));
                Assert.That(project.Members[0].Role, Is.EqualTo(role));
            });
        }

        [Test]
        public void AddMember_WithNullUser_ShouldThrowArgumentNullException()
        {
            var project = new Project("SOFA3", "Beschrijving");

            Assert.Throws<ArgumentNullException>(() => project.AddMember(null!, ProjectRole.DEVELOPER));
        }

        [Test]
        public void CreateBacklogItem_WithValidData_ShouldCreateAndAddItemToBacklog()
        {
            var project = new Project("SOFA3", "Beschrijving");
            var title = "Nieuw backlog item";
            var description = "Backlog item beschrijving";

            var item = project.CreateBacklogItem(title, description);

            Assert.Multiple(() =>
            {
                Assert.That(item, Is.Not.Null);
                Assert.That(item.Title, Is.EqualTo(title));
                Assert.That(item.Description, Is.EqualTo(description));
                Assert.That(item.ProjectId, Is.EqualTo(project.ProjectId));
                Assert.That(item.State, Is.TypeOf<ToDoState>());
                Assert.That(project.Backlog.Items, Does.Contain(item));
            });
        }

        [Test]
        public void CreateBacklogItem_WithNullDescription_ShouldSetDescriptionToEmptyString()
        {
            var project = new Project("SOFA3", "Beschrijving");

            var item = project.CreateBacklogItem("Nieuw item", null!);

            Assert.That(item.Description, Is.EqualTo(string.Empty));
        }

        [Test]
        public void CreateBacklogItem_WithNullTitle_ShouldThrowArgumentException()
        {
            var project = new Project("SOFA3", "Beschrijving");

            var ex = Assert.Throws<ArgumentException>(() => project.CreateBacklogItem(null!, "Beschrijving"));

            Assert.Multiple(() =>
            {
                Assert.That(ex!.Message, Does.Contain("Backlog item title is required."));
                Assert.That(ex.ParamName, Is.EqualTo("title"));
            });
        }

        [Test]
        public void CreateBacklogItem_WithEmptyTitle_ShouldThrowArgumentException()
        {
            var project = new Project("SOFA3", "Beschrijving");

            var ex = Assert.Throws<ArgumentException>(() => project.CreateBacklogItem("", "Beschrijving"));

            Assert.Multiple(() =>
            {
                Assert.That(ex!.Message, Does.Contain("Backlog item title is required."));
                Assert.That(ex.ParamName, Is.EqualTo("title"));
            });
        }

        [Test]
        public void CreateBacklogItem_WithWhitespaceTitle_ShouldThrowArgumentException()
        {
            var project = new Project("SOFA3", "Beschrijving");

            var ex = Assert.Throws<ArgumentException>(() => project.CreateBacklogItem("   ", "Beschrijving"));

            Assert.Multiple(() =>
            {
                Assert.That(ex!.Message, Does.Contain("Backlog item title is required."));
                Assert.That(ex.ParamName, Is.EqualTo("title"));
            });
        }

        [Test]
        public void CreateSprint_WithValidData_ShouldNotThrowException()
        {
            var name = "Sprint 1";
            var startDate = new DateOnly(2026, 4, 1);
            var endDate = new DateOnly(2026, 4, 14);

            Assert.DoesNotThrow(() => Project.CreateSprint(name, startDate, endDate));
        }

        [Test]
        public void CreateSprint_WithNullName_ShouldThrowArgumentException()
        {
            var startDate = new DateOnly(2026, 4, 1);
            var endDate = new DateOnly(2026, 4, 14);

            var ex = Assert.Throws<ArgumentException>(() => Project.CreateSprint(null!, startDate, endDate));

            Assert.That(ex!.Message, Does.Contain("Sprint name is required."));
        }

        [Test]
        public void CreateSprint_WithEmptyName_ShouldThrowArgumentException()
        {
            var startDate = new DateOnly(2026, 4, 1);
            var endDate = new DateOnly(2026, 4, 14);

            var ex = Assert.Throws<ArgumentException>(() => Project.CreateSprint("", startDate, endDate));

            Assert.That(ex!.Message, Does.Contain("Sprint name is required."));
        }

        [Test]
        public void CreateSprint_WithEndDateBeforeStartDate_ShouldThrowArgumentException()
        {
            var startDate = new DateOnly(2026, 4, 14);
            var endDate = new DateOnly(2026, 4, 1);

            var ex = Assert.Throws<ArgumentException>(() => Project.CreateSprint("Sprint 1", startDate, endDate));

            Assert.That(ex!.Message, Does.Contain("End date cannot be before start date."));
        }
    }
}