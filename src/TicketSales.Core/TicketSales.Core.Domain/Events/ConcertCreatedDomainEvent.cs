using System;
using System.Collections.Generic;
using System.Text;

namespace TicketSales.Core.Domain.Events
{
    public class ConcertCreatedDomainEvent : DomainEvent
    {
        public int NumberOfTickets { get; }

        public string Place { get; }

        public DateTime Date { get; }

        public string Title { get; }

        public Guid ConcertId { get; }


        public ConcertCreatedDomainEvent(int numberOfTickets, string place, DateTime timeAndDate, string title, Guid concertId)
        {
            NumberOfTickets = numberOfTickets;
            Place = place;
            Date = timeAndDate;
            Title = title;
            ConcertId = concertId;
        }
    }
}
