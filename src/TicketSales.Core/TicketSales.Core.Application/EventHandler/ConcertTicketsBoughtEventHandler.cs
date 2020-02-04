using System;
using System.Collections.Generic;
using System.Text;
using MassTransit;
using TicketSales.Core.Domain;
using TicketSales.Core.Domain.Events;
using TicketSales.Messages.Events;

namespace TicketSales.Core.Application.EventHandler
{
    public class ConcertTicketsBoughtEventHandler : IEventPublisher 
    {
        public void Publish(DomainEvent @event, IPublishEndpoint publisher)
        {
            ConcertTicketsBoughtDomainEvent ticketsBoughtEvent = (ConcertTicketsBoughtDomainEvent)@event;

            ConcertTicketsBoughtEvent concertTicketsBought = new ConcertTicketsBoughtEvent(ticketsBoughtEvent.NumberOfTicketsToBuy, ticketsBoughtEvent.UserId, ticketsBoughtEvent.ConcertId);

            publisher.Publish(concertTicketsBought);
        }
    }
}
