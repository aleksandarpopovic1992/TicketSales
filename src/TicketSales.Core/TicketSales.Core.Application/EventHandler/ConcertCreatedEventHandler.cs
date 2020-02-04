using System;
using System.Collections.Generic;
using System.Text;
using MassTransit;
using TicketSales.Core.Domain;
using TicketSales.Core.Domain.Events;
using TicketSales.Messages.Events;

namespace TicketSales.Core.Application.EventHandler
{
    public class ConcertCreatedEventHandler : IEventPublisher  
    {
        public void Publish(DomainEvent @event, IPublishEndpoint publisher)
        {
            ConcertCreatedDomainEvent createdDomainEvent = (ConcertCreatedDomainEvent)@event;

            ConcertCreatedEvent concertCreatedEvent = new ConcertCreatedEvent(createdDomainEvent.NumberOfTickets, createdDomainEvent.Place, createdDomainEvent.Date, createdDomainEvent.Title, createdDomainEvent.ConcertId);

            publisher.Publish(concertCreatedEvent);
        }
    }
}
