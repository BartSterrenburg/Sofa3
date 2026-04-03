using Sofa3.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.Core.Test
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void Constructor_WithValidData_ShouldCreateUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var name = "Bart";
            var email = "bart@mail.com";

            // Act
            var user = new User(userId, name, email);

            // Assert
            Assert.That(user, Is.Not.Null);
            Assert.That(user.UserId, Is.EqualTo(userId));
            Assert.That(user.Name, Is.EqualTo(name));
            Assert.That(user.Email, Is.EqualTo(email));
        }

        [Test]
        public void Constructor_WithEmptyName_ShouldStillAssignName()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var name = "";
            var email = "bart@mail.com";

            // Act
            var user = new User(userId, name, email);

            // Assert
            Assert.That(user.Name, Is.EqualTo(name));
        }

        [Test]
        public void Constructor_WithNullName_ShouldAllowNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            string name = null!;
            var email = "bart@mail.com";

            // Act
            var user = new User(userId, name, email);

            // Assert
            Assert.That(user.Name, Is.Null);
        }

        [Test]
        public void Constructor_WithEmptyEmail_ShouldStillAssignEmail()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var name = "Bart";
            var email = "";

            // Act
            var user = new User(userId, name, email);

            // Assert
            Assert.That(user.Email, Is.EqualTo(email));
        }

        [Test]
        public void Constructor_WithNullEmail_ShouldAllowNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var name = "Bart";
            string email = null!;

            // Act
            var user = new User(userId, name, email);

            // Assert
            Assert.That(user.Email, Is.Null);
        }

        [Test]
        public void Constructor_ShouldAssignDifferentUsersCorrectly()
        {
            // Arrange
            var user1 = new User(Guid.NewGuid(), "User1", "user1@mail.com");
            var user2 = new User(Guid.NewGuid(), "User2", "user2@mail.com");

            // Assert
            Assert.That(user1.UserId, Is.Not.EqualTo(user2.UserId));
            Assert.That(user1.Name, Is.Not.EqualTo(user2.Name));
            Assert.That(user1.Email, Is.Not.EqualTo(user2.Email));
        }
    }
}
