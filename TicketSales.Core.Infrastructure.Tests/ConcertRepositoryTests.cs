using Moq;
using NEventStore;
using System;
using System.Collections.Generic;
using System.Text;
using TicketSales.Core.Domain;
using TicketSales.Core.Domain.Events;
using TicketSales.Core.Domain.Factories;
using TicketSales.Core.Infrastructe;
using Xunit;

namespace TicketSales.Core.Infrastructure.Tests
{
    public class ConcertRepositoryTests
    {


        [Fact]
        public void ConcertRepository_Add()
        {
            Mock<IStoreEvents> storeEvents = new Mock<IStoreEvents>();
            Mock<IEventStream> eventStream = new Mock<IEventStream>();
            Mock<IConcertFactory> concertFactory = new Mock<IConcertFactory>();
            storeEvents.Setup(x => x.CreateStream(It.IsAny<string>(),It.IsAny<string>())).Returns(eventStream.Object);
            Concert concert = new Concert("SPENS", 50, DateTime.Now, "Koncert godine", Guid.NewGuid());
            ConcertRepository repository = new ConcertRepository(storeEvents.Object,concertFactory.Object);
            repository.Add(concert);
            eventStream.Verify(x => x.Add(It.IsAny<EventMessage>()), Times.Once);
            eventStream.Verify(x => x.CommitChanges(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void ConcertRepository_TryFindBy_False()
        {
            Mock<IStoreEvents> storeEvents = new Mock<IStoreEvents>();
            Mock<IEventStream> eventStream = new Mock<IEventStream>();

            eventStream.Setup(x => x.CommittedEvents).Returns(new List<EventMessage>());
            Mock<IConcertFactory> concertFactory = new Mock<IConcertFactory>();
            concertFactory.Setup(x => x.Create()).Returns(new Concert());
            storeEvents.Setup(x => x.OpenStream(It.IsAny<string>(), It.IsAny<string>(),0,int.MaxValue)).Returns(eventStream.Object);

            ConcertRepository repository = new ConcertRepository(storeEvents.Object, concertFactory.Object);
            Concert concert;

            bool result = repository.TryFindBy(Guid.NewGuid(),out concert);

            Assert.False(result);
        }

        [Fact]
        public void ConcertRepository_TryFindBy_True()
        {
            Mock<IStoreEvents> storeEvents = new Mock<IStoreEvents>();
            Mock<IEventStream> eventStream = new Mock<IEventStream>();
            ConcertCreatedDomainEvent concertCreated = new ConcertCreatedDomainEvent(3, "SPENS", DateTime.Now, "Koncert godine", Guid.NewGuid());
            eventStream.Setup(x => x.CommittedEvents).Returns(new List<EventMessage>() { new EventMessage() { Body = concertCreated } });
           
            Mock<IConcertFactory> concertFactory = new Mock<IConcertFactory>();
            concertFactory.Setup(x => x.Create()).Returns(new Concert());
            storeEvents.Setup(x => x.OpenStream(It.IsAny<string>(), It.IsAny<string>(), 0, int.MaxValue)).Returns(eventStream.Object);

            ConcertRepository repository = new ConcertRepository(storeEvents.Object, concertFactory.Object);
            Concert concert;

            bool result = repository.TryFindBy(Guid.NewGuid(), out concert);

            Assert.True(result);

        }

    }


}
