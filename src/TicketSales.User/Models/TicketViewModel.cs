using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSales.Shared;

namespace TicketSales.User.Models
{
    public class TicketViewModel 
    {
        public int NumberOfTickets { get; set; }

        public Guid ConcertId { get; set; }

        public string ConcertTitle { get; set; }
    }
}
