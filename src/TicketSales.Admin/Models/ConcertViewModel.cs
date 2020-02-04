using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSales.Admin.Models
{
    public class ConcertViewModel
    {
        public string Title { get; set; }

        public Guid Id { get; set; }

        public int TicketsSoldOut { get; set;}

        public int MaximumNumberOfTickets { get; set; }
    }
}
