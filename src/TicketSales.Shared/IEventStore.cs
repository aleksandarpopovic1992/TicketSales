using System;
using System.Collections.Generic;
using System.Text;

namespace TicketSales.Shared
{
    //dummy event store
    public interface IEventStore
    {
        void Add(IEvent @event);
    }
}
