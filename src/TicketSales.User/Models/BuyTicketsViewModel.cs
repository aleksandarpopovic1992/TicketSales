using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TicketSales.User.Models.Validation;

namespace TicketSales.User.Models
{
    public class BuyTicketsViewModel
    {

        public int TicketNumber { get; set; }

        public long UserId { get; set; }

        [NotEmpty]
        public Guid ConcertId { get; set; }

        public IEnumerable<SelectListItem> ConcertIds { get; set; }

    }
}
