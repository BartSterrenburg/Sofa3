using Sofa3.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.Core.Test
{
    [TestFixture]
    public class ProjectMembershipTests
    {
        [Test]
        public void Constructor_WithValidData_ShouldCreateMembership()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var role = ProjectRole.DEVELOPER;

            // Act
            var membership = new ProjectMembership(userId, role);

            // Assert
            Assert.That(membership, Is.Not.Null);
            Assert.That(membership.MembershipId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(membership.UserId, Is.EqualTo(userId));
            Assert.That(membership.Role, Is.EqualTo(role));
        }

        [Test]
        public void Constructor_ShouldGenerateUniqueMembershipId()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act
            var membership1 = new ProjectMembership(userId, ProjectRole.DEVELOPER);
            var membership2 = new ProjectMembership(userId, ProjectRole.DEVELOPER);

            // Assert
            Assert.That(membership1.MembershipId, Is.Not.EqualTo(membership2.MembershipId));
        }

        [Test]
        public void Constructor_ShouldAssignCorrectRole()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act
            var membership = new ProjectMembership(userId, ProjectRole.SCRUM_MASTER);

            // Assert
            Assert.That(membership.Role, Is.EqualTo(ProjectRole.SCRUM_MASTER));
        }

        [Test]
        public void Constructor_ShouldAssignCorrectUserId()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act
            var membership = new ProjectMembership(userId, ProjectRole.PRODUCT_OWNER);

            // Assert
            Assert.That(membership.UserId, Is.EqualTo(userId));
        }
    }
}
