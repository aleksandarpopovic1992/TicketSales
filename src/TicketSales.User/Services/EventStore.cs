using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSales.Shared;

namespace TicketSales.User.Services
{
    public class EventStore : IEventStore
    {
        private IList<IEvent> _events;
        private static readonly object _object = new object();

        public EventStore()
        {
            _events = new List<IEvent>(); 
        }

        public void Add(IEvent @event)
        {
            lock (_object)
            {
                _events.Add(@event);
            }
        }
    }
}
