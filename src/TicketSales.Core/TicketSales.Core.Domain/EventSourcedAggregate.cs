using System;
using System.Collections.Generic;
using System.Text;

namespace TicketSales.Core.Domain
{
    public abstract class EventSourcedAggregate : Entity
    {
        public IList<DomainEvent> Changes { get; private set; }
        public int Version { get; protected set; }

        public EventSourcedAggregate()
        {
            Changes = new List<DomainEvent>();
        }

        public abstract void Apply(DomainEvent changes);
    }
}
