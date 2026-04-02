using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Core
{
    public class Message
    {
        public Guid MessageId { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Message(string content)
        {
            MessageId = Guid.NewGuid();
            Content = content;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
