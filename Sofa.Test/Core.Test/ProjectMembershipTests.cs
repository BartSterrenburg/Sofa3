using Sofa3.Domain.Core;
using System;
using NUnit.Framework;

namespace TestProject1.Core.Test
{
    [TestFixture]
    public class ProjectMembershipTests
    {
        [Test]
        public void Constructor_WithValidData_ShouldCreateMembership()
        {
            var userId = Guid.NewGuid();
            var role = ProjectRole.DEVELOPER;

            var membership = new ProjectMembership(userId, role);

            Assert.Multiple(() =>
            {
                Assert.That(membership, Is.Not.Null);
                Assert.That(membership.MembershipId, Is.Not.EqualTo(Guid.Empty));
                Assert.That(membership.UserId, Is.EqualTo(userId));
                Assert.That(membership.Role, Is.EqualTo(role));
            });
        }

        [Test]
        public void Constructor_ShouldGenerateUniqueMembershipId()
        {
            var userId = Guid.NewGuid();

            var membership1 = new ProjectMembership(userId, ProjectRole.DEVELOPER);
            var membership2 = new ProjectMembership(userId, ProjectRole.DEVELOPER);

            Assert.That(membership1.MembershipId, Is.Not.EqualTo(membership2.MembershipId));
        }

        [Test]
        public void Constructor_ShouldAssignCorrectRole()
        {
            var userId = Guid.NewGuid();

            var membership = new ProjectMembership(userId, ProjectRole.SCRUM_MASTER);

            Assert.That(membership.Role, Is.EqualTo(ProjectRole.SCRUM_MASTER));
        }

        [Test]
        public void Constructor_ShouldAssignCorrectUserId()
        {
            var userId = Guid.NewGuid();

            var membership = new ProjectMembership(userId, ProjectRole.PRODUCT_OWNER);

            Assert.That(membership.UserId, Is.EqualTo(userId));
        }
    }
}