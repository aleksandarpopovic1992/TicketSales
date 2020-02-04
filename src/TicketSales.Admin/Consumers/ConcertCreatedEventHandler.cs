using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSales.Admin.Models;
using TicketSales.Messages.Events;
using TicketSales.Shared;
using TicketSales.Shared.Factory;

namespace TicketSales.Admin.Consumers
{
    public class ConcertCreatedEventHandler : IConsumer<ConcertCreatedEvent>
    {
        private IProjectorFactory<ConcertCreatedEvent> _projectorFactory;
        private IEventStore _eventStore;


        public ConcertCreatedEventHandler(IProjectorFactory<ConcertCreatedEvent> projectorFactory, IEventStore eventStore)
        {
            _projectorFactory = projectorFactory;
            _eventStore = eventStore;
        }

        public Task Consume(ConsumeContext<ConcertCreatedEvent> context)
        {
            var projector = _projectorFactory.CreateProjector();
            projector.Project(context.Message);
            _eventStore.Add(context.Message);
            return Task.CompletedTask;
        }
    }
}
