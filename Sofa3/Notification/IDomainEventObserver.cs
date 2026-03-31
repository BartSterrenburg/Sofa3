using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.Notification
{
    public interface IDomainEventObserver
    {
        void Update(DomainEvent domainEvent);
    }
}
