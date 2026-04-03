using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Core
{
    public class Backlog
    {
        public Guid BacklogId { get; private set; }
        public Guid ProjectId { get; private set; }
        public string Name { get; private set; }
        public List<BacklogItem> Items { get; private set; } = new();

        public Backlog(Guid backlogId, Guid projectId, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Backlog name is required.", nameof(name));
            }
            BacklogId = backlogId;
            ProjectId = projectId;
            Name = name;
        }

        public void AddItem(BacklogItem item)
        {
            ArgumentNullException.ThrowIfNull(item);

            if (item.ProjectId != ProjectId)
            {
                throw new InvalidOperationException("Backlog item belongs to a different project.");
            }

            this.Items.Add(item);
        }

        public void RemoveItem(Guid itemId)
        {
            var item = this.Items.FirstOrDefault(i => i.BacklogItemId == itemId);
            if (item != null)
            {
                this.Items.Remove(item);
            }
        }
    }
}
