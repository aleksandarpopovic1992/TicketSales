using System;
using System.Collections.Generic;
using System.Text;

namespace TicketSales.Core.Domain.Factories
{
    public class ConcertFactory : IConcertFactory
    {

        public Concert Create(string place, int maximumNumberOfTickets, DateTime date, string title, Guid concertId)
        {
            return new Concert(place,maximumNumberOfTickets,date,title,concertId);
        }

        public Concert Create()
        {
            return new Concert();
        }
    }
}
