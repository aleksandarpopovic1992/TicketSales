using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using TicketSales.Core.Domain;

namespace TicketSales.Core.Application.EventHandler
{

    // napraviti genericki za @event
    public interface IEventPublisherFactory
    {
        IEventPublisher CreatePublisher(DomainEvent @event);     
    }
}
