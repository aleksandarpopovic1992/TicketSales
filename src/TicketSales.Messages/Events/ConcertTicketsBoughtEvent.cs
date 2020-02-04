using System;
using System.Collections.Generic;
using System.Text;
using TicketSales.Shared;

namespace TicketSales.Messages.Events
{
    public class ConcertTicketsBoughtEvent : IEvent
    {
        public int NumberOfTickets { get; }

        public long UserId { get; }

        public Guid ConcertId { get; }

        public ConcertTicketsBoughtEvent(int numberOfTickets, long userId, Guid concertId)
        {
            NumberOfTickets = numberOfTickets;

            UserId = userId;

            ConcertId = concertId;
        }

    }
}
