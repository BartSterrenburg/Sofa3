using Sofa3.Domain.Core;
using System;
using NUnit.Framework;

namespace TestProject1.Core.Test
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void Constructor_WithValidData_ShouldCreateUser()
        {
            var userId = Guid.NewGuid();
            var name = "Bart";
            var email = "bart@mail.com";

            var user = new User(userId, name, email);

            Assert.Multiple(() =>
            {
                Assert.That(user, Is.Not.Null);
                Assert.That(user.UserId, Is.EqualTo(userId));
                Assert.That(user.Name, Is.EqualTo(name));
                Assert.That(user.Email, Is.EqualTo(email));
            });
        }

        [Test]
        public void Constructor_WithEmptyName_ShouldStillAssignName()
        {
            var userId = Guid.NewGuid();
            var name = "";
            var email = "bart@mail.com";

            var user = new User(userId, name, email);

            Assert.That(user.Name, Is.EqualTo(name));
        }

        [Test]
        public void Constructor_WithNullName_ShouldAllowNull()
        {
            var userId = Guid.NewGuid();
            string name = null!;
            var email = "bart@mail.com";

            var user = new User(userId, name, email);

            Assert.That(user.Name, Is.Null);
        }

        [Test]
        public void Constructor_WithEmptyEmail_ShouldStillAssignEmail()
        {
            var userId = Guid.NewGuid();
            var name = "Bart";
            var email = "";

            var user = new User(userId, name, email);

            Assert.That(user.Email, Is.EqualTo(email));
        }

        [Test]
        public void Constructor_WithNullEmail_ShouldAllowNull()
        {
            var userId = Guid.NewGuid();
            var name = "Bart";
            string email = null!;

            var user = new User(userId, name, email);

            Assert.That(user.Email, Is.Null);
        }

        [Test]
        public void Constructor_ShouldAssignDifferentUsersCorrectly()
        {
            var user1 = new User(Guid.NewGuid(), "User1", "user1@mail.com");
            var user2 = new User(Guid.NewGuid(), "User2", "user2@mail.com");

            Assert.Multiple(() =>
            {
                Assert.That(user1.UserId, Is.Not.EqualTo(user2.UserId));
                Assert.That(user1.Name, Is.Not.EqualTo(user2.Name));
                Assert.That(user1.Email, Is.Not.EqualTo(user2.Email));
            });
        }
    }
}