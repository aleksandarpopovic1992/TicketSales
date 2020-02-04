using System;
using System.Collections.Generic;
using System.Text;
using TicketSales.Core.Domain;
using TicketSales.Core.Domain.Events;

namespace TicketSales.Core.Application.EventHandler
{
    public class EventHandlerFactory : IEventPublisherFactory
    {
        private Dictionary<Type, IEventPublisher> _publishers;

        public EventHandlerFactory(Dictionary<Type, IEventPublisher> publishers)
        {
            _publishers = publishers;
        }

        public IEventPublisher CreatePublisher(DomainEvent @event)
        {
            return _publishers[@event.GetType()];
        }
    }
}
