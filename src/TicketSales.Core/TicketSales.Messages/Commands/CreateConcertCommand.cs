using System;
using System.Collections.Generic;
using System.Text;

namespace TicketSales.Messages.Commands
{
    public class CreateConcertCommand
    {
        public int MaximumNumberOfTickets { get;}

        public string Place { get; }

        public DateTime Date{ get;  }

        public string Title { get; }


        public CreateConcertCommand(int maximumNumberOfTickets, string place, DateTime date, string title)
        {
            MaximumNumberOfTickets = maximumNumberOfTickets;
            Place = place;
            Date = date;
            Title = title;
        }

    }
}
