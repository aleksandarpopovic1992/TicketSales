using System;
using System.Collections.Generic;
using System.Text;
using TicketSales.Core.Domain;
using Xunit;

namespace TicketsSale.Core.Domain.Tests
{
    public class TicketsTest
    {

        [Fact]
        public void Tickets_HasMoreTickets_True()
        {
            Tickets tickets = new Tickets(5);
            Tickets ticketsTwo = new Tickets(4);

            Assert.True(tickets.HasMoreThan(ticketsTwo));
        }

        [Fact]
        public void Tickets_HasMoreTickets_False()
        {
            Tickets tickets = new Tickets(4);
            Tickets ticketsTwo = new Tickets(5);

            Assert.False(tickets.HasMoreThan(ticketsTwo));
        }

        [Fact]
        public void Tickets_AddNumberOfTickets()
        {
            Tickets tickets = new Tickets(4);
            Tickets ticketsTwo = new Tickets(5);

            Tickets newTickets = tickets.AddNumberOf(ticketsTwo);

            Assert.True(newTickets.Quantity == 9);
        }


    }
}
