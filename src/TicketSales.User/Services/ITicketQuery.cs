using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSales.Shared;
using TicketSales.User.Models;

namespace TicketSales.User.Services
{
    public interface ITicketQuery : IQuery
    {
        IEnumerable<TicketViewModel> GetTicketsBy(long userId);
    }
}
