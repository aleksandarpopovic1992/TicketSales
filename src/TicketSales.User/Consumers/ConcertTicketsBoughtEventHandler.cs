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
    public class ConcertTicketsBoughtEventHandler : IConsumer<ConcertTicketsBoughtEvent>
    {
        private IProjectorFactory<ConcertTicketsBoughtEvent> _projectorFactory;
        private IEventStore _eventStore;

        public ConcertTicketsBoughtEventHandler(IProjectorFactory<ConcertTicketsBoughtEvent> projectorFactory,IEventStore eventStore)
        {
            _projectorFactory = projectorFactory;
            _eventStore = eventStore;
        }

        public Task Consume(ConsumeContext<ConcertTicketsBoughtEvent> context)
        {
            var projector = _projectorFactory.CreateProjector();
            projector.Project(context.Message);
            _eventStore.Add(context.Message);
            return Task.CompletedTask;
        }
    }
}
