using System;
using System.Collections.Generic;
using System.Text;

namespace TicketSales.Core.Domain.Factories
{
    public interface IConcertFactory
    {
        Concert Create(string place, int maximumNumberOfTickets, DateTime date, string title, Guid concertId);
        Concert Create();
    }
}
