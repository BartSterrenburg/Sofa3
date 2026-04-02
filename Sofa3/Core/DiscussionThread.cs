using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Core
{
    public class DiscussionThread
    {
        public Guid ThreadId { get; private set; }
        public string Subject { get; private set; }
        public bool IsClosed { get; private set; }

        private readonly List<Message> _messages = new();
        public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();

        public DiscussionThread(string subject)
        {
            ThreadId = Guid.NewGuid();
            Subject = subject;
            IsClosed = false;
        }

        public void AddMessage(Message message)
        {
            if (IsClosed)
                throw new InvalidOperationException("Thread is gesloten.");

            if (message == null)
                throw new ArgumentNullException(nameof(message));

            _messages.Add(message);
        }

        public void CloseThread()
        {
            IsClosed = true;
        }

        public void ReopenThread()
        {
            IsClosed = false;
        }
    }
}
