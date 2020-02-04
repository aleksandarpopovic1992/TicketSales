using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSales.Admin.Models;
using TicketSales.Shared;

namespace TicketSales.Admin.Services
{
    public interface IConcertQuery : IQuery
    {
        IEnumerable<ConcertViewModel> GetConcerts();
    }
}
