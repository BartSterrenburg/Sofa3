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
        public string Name { get; private set; }
        public List<BacklogItem> Items { get; private set; } = new();

        public Backlog(Guid backlogId, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Backlog name is required.", nameof(name));
            }
            BacklogId = backlogId;
            Name = name;
        }

        public void AddItem(BacklogItem item)
        {
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
