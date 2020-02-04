using MassTransit;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TicketSales.Core.Application.EventHandler;
using TicketSales.Core.Domain;
using TicketSales.Core.Domain.Repositories;
using TicketSales.Messages.Commands;
using Xunit;

namespace TicketSales.Core.Application.Tests
{
    public class BuyTicketsForConcertCommandHandlerTests
    {

        [Fact]
        public void BuyTickets_ConcertDoesNotExists()
        {
            Mock<ConsumeContext<BuyConcertTicketsCommand>> context = new Mock<ConsumeContext<BuyConcertTicketsCommand>>();
            Guid guid = Guid.NewGuid();
            BuyConcertTicketsCommand concertTicketCommand = new BuyConcertTicketsCommand(2,5,guid);
            context.Setup(x => x.Message).Returns(concertTicketCommand);

            Mock<IConcertRepository> concertRepository = new Mock<IConcertRepository>();
            Concert concert;
            concertRepository.Setup(x => x.TryFindBy(guid, out concert)).Returns(false);

            Mock<IEventPublisherFactory> eventHandlerFactory = new Mock<IEventPublisherFactory>();
            Mock<ILogger> logger = new Mock<ILogger>();

            BuyConcertTicketsCommandHandler commandHandler = new BuyConcertTicketsCommandHandler(concertRepository.Object,eventHandlerFactory.Object,logger.Object);

            commandHandler.Consume(context.Object);

            concertRepository.Verify(x => x.Save(It.IsAny<Concert>()), Times.Never);
        }

        [Fact]
        public void BuyTickets_TicketsBought()
        {
            Mock<ConsumeContext<BuyConcertTicketsCommand>> context = new Mock<ConsumeContext<BuyConcertTicketsCommand>>();
            Guid concertGuid = Guid.NewGuid();

            int numberOfBoughtTickets = 2;


            BuyConcertTicketsCommand concertTicketCommand = new BuyConcertTicketsCommand(2, 5, concertGuid);
            context.Setup(x => x.Message).Returns(concertTicketCommand);

            Mock<IConcertRepository> concertRepository = new Mock<IConcertRepository>();
            Concert concert = new Concert("SPENS,Novi sad",5000,DateTime.Now,"Concert of the year", concertGuid);
            concertRepository.Setup(x => x.TryFindBy(concertGuid, out concert)).Returns(true);
            Mock<ILogger> logger = new Mock<ILogger>();

            Mock<IEventPublisherFactory> eventPublisherFactory = new Mock<IEventPublisherFactory>();
            Mock<IEventPublisher> eventPublisher = new Mock<IEventPublisher>();
            eventPublisherFactory.Setup(x => x.CreatePublisher(It.IsAny<DomainEvent>())).Returns(eventPublisher.Object);

            BuyConcertTicketsCommandHandler commandHandler = new BuyConcertTicketsCommandHandler(concertRepository.Object, eventPublisherFactory.Object, logger.Object);

            commandHandler.Consume(context.Object);

            Assert.True(concert.BoughtTickets.Quantity == numberOfBoughtTickets);

            

        }



    }


}
