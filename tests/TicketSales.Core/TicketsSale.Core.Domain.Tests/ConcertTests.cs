using System;
using System.Collections.Generic;
using System.Text;
using TicketSales.Core.Domain;
using TicketSales.Core.Domain.Events;
using TicketSales.Core.Domain.Exception;
using Xunit;

namespace TicketsSale.Core.Domain.Tests
{
    public class ConcertTests
    {

        [Fact]
        public void Concert_ConstructionFailed()
        {

            Assert.Throws<IntialTicketsCapacityNotSet>(() =>CreateConcert(0));
        }

        [Fact]
        public void Concert_ConstructionSuccess()
        {


            Concert concert = CreateConcert(3);

            Assert.True(concert.BoughtTickets.Quantity == 0);
            Assert.True(concert.MaximumNumberOfTickets.Quantity == 3);
        }

        [Fact]
        public void Concert_BuyTickets_MaximumNumnberOfTicketsReached()
        {
            Concert concert = CreateConcert(3);

            Assert.Throws<MaximumNumberOfTicketsReached>(() => concert.BuyTickets(4,3));           
        }

        [Fact]
        public void Concert_BuyTickets_TicketsBuySuccessfull()
        {
            Concert concert = CreateConcert(3);

            concert.BuyTickets(2,3);

            Assert.True(concert.BoughtTickets.Quantity == 2);
        }


        private Concert CreateConcert(int maximumNumberOfTickets)
        {
            string place = "Something";
            string name = "Concert Of the Year";
            DateTime concertTime = DateTime.Now;
            Guid guid = Guid.NewGuid();
            return new Concert(place, maximumNumberOfTickets, concertTime, name, guid);
        }

        [Fact]
        public void Concert_Applying_ConcertCreatedDomainEvent()
        {
            ConcertCreatedDomainEvent concertCreated = new ConcertCreatedDomainEvent(3,"SPENS",DateTime.Now,"Koncert godine",Guid.NewGuid());
            Concert concert = new Concert();
            concert.Apply(concertCreated);
            Assert.True(concert.BoughtTickets.Quantity == 0);
            Assert.True(concert.Date == concertCreated.Date);
            Assert.True(concert.Id == concertCreated.ConcertId);
            Assert.True(concert.MaximumNumberOfTickets.Quantity == concertCreated.NumberOfTickets);
            
        }

        [Fact]
        public void Concert_Applying_ConcertTicketsBoughtDomainEvent()
        {
            ConcertTicketsBoughtDomainEvent ticketsBought = new ConcertTicketsBoughtDomainEvent(10,5,Guid.NewGuid());
            Concert concert = new Concert("SPENS",50, DateTime.Now, "Koncert godine", Guid.NewGuid());
            concert.Apply(ticketsBought);
            Assert.True(concert.BoughtTickets.Quantity == 10);
        }


    }

}

