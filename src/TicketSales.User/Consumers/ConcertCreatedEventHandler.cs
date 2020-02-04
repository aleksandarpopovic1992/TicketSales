using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSales.Messages.Events;
using TicketSales.Shared;
using TicketSales.Shared.Factory;
using TicketSales.User.Models;


namespace TicketSales.User.Consumers
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
            _eventStore.Add(context.Message); // posto se radi projekcija cim stigne event, ovde se samo smestaju eventi u dummy bazu
            return Task.CompletedTask;
        }
    }
}
