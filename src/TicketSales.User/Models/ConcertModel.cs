using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSales.Shared;

namespace TicketSales.User.Models
{
    public class ConcertModel 
    {
        public Guid Id { get; }

        public string Title { get; }

        public ConcertModel(Guid id, string title)
        {
            Id = id;
            Title = title;
        }

    }
}
