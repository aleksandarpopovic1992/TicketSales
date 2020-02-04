using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSales.Admin.Models
{
    public class CreateConcertViewModel
    {

        public int NumberOfTickets { get; set; }

        public string Place { get; set; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 1)]
        public string Title { get; set; }
    }
}
