using System;
using System.Collections.Generic;
using System.Text;
using TicketSales.Shared;

namespace TicketSales.Messages.Events
{
    public class ConcertCreatedEvent : IEvent
    {
        public int NumberOfTickets { get; }

        public string Place { get; }

        public DateTime Date { get; }

        public string Title { get; }

        public Guid ConcertId { get; }


        public ConcertCreatedEvent(int numberOfTickets, string place, DateTime timeAndDate, string title, Guid concertId)
        {
            NumberOfTickets = numberOfTickets;
            Place = place;
            Date = timeAndDate;
            Title = title;
            ConcertId = concertId;
        }


    }
}
