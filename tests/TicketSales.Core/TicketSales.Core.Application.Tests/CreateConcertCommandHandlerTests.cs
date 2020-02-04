using MassTransit;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TicketSales.Core.Application.EventHandler;
using TicketSales.Core.Domain;
using TicketSales.Core.Domain.Events;
using TicketSales.Core.Domain.Factories;
using TicketSales.Core.Domain.Repositories;
using TicketSales.Messages.Commands;
using TicketSales.Messages.Events;
using Xunit;

namespace TicketSales.Core.Application.Tests
{
    public class CreateConcertCommandHandlerTests
    {
        [Fact]
        public void CreateConcertCommandHandler_Success()
        {
            Mock<IConcertRepository> concertRepository = new Mock<IConcertRepository>();
            Mock<IEventPublisherFactory> eventHandlerFactory = new Mock<IEventPublisherFactory>();
            Mock<IConcertFactory> concertFactory = new Mock<IConcertFactory>();
            Guid concertId = Guid.NewGuid();
            concertRepository.Setup(x => x.GetNextId()).Returns(concertId); 

            CreateConcertCommand createConcertCommand = new CreateConcertCommand(2500,"SPENS,Novi Sad",DateTime.Now,"Koncert godine");
            Concert concert = new Concert(createConcertCommand.Place,createConcertCommand.MaximumNumberOfTickets,createConcertCommand.Date,createConcertCommand.Title,concertId);
            concertFactory.Setup(x => x.Create(createConcertCommand.Place, createConcertCommand.MaximumNumberOfTickets, createConcertCommand.Date, createConcertCommand.Title, concertId)).Returns(concert);
            Mock<ConsumeContext<CreateConcertCommand>> context = new Mock<ConsumeContext<CreateConcertCommand>>();
            context.Setup(x => x.Message).Returns(createConcertCommand);
            Mock<ILogger> logger = new Mock<ILogger>();
            CreateConcertCommandHandler createConcertCommandHandler = new CreateConcertCommandHandler(concertRepository.Object,eventHandlerFactory.Object,concertFactory.Object,logger.Object);
            Assert.True(concert.Changes.Count == 1);
            var createdEvent = (ConcertCreatedDomainEvent) concert.Changes[0];

            Assert.True(createdEvent.ConcertId == concertId);
            Assert.True(createdEvent.Date == createConcertCommand.Date);
            Assert.True(createdEvent.NumberOfTickets == createConcertCommand.MaximumNumberOfTickets);
            Assert.True(createdEvent.Place == createConcertCommand.Place);
            Assert.True(createdEvent.Title == createConcertCommand.Title);

        }

    }
}
