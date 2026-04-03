using Sofa3.Domain.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Core
{
    public class Project
    {
        public Guid ProjectId { get; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; }
        public List<ProjectMembership> Members { get; private set; } = new();

        public Project(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Project name is required.", nameof(name));

            ProjectId = Guid.NewGuid();
            Name = name;
            Description = description ?? string.Empty;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddMember(User user, ProjectRole role)
        {
            ArgumentNullException.ThrowIfNull(user);

            Members.Add(new ProjectMembership(user.UserId, role));
        }

        public static void CreateBacklogItem(string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Backlog item title is required.", nameof(title));

            // Aanmaaklogica komt hier.
            // Geen BacklogItem-object returnen omdat je geen extra uitwerking wilt toevoegen.
        }

        public static void CreateSprint(string name, DateOnly startDate, DateOnly endDate)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Sprint name is required.", nameof(name));

            if (endDate < startDate)
                throw new ArgumentException("End date cannot be before start date.");

            // Aanmaaklogica komt hier.
        }
    }
}
