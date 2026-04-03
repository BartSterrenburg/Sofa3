using Sofa3.Domain.Notification;
using Sofa3.Domain.Notification.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Core
{
    public class DiscussionThread : AggregateRoot
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

            ArgumentNullException.ThrowIfNull(message);

            _messages.Add(message);
            AddDomainEvent(new DiscussionMessageAddedEvent(message.Content));
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
