using System;
using System.Collections.Generic;
using System.Text;

namespace TicketSales.Core.Domain.Exception
{
    public class MaximumNumberOfTicketsReached : System.Exception
    {
        public MaximumNumberOfTicketsReached(string message) : base(message)
        {
        }
    }
}
