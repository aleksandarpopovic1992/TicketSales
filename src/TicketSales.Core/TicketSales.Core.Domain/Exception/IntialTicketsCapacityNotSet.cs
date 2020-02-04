using System;
using System.Collections.Generic;
using System.Text;

namespace TicketSales.Core.Domain.Exception
{
    public class IntialTicketsCapacityNotSet : System.Exception
    {
        public IntialTicketsCapacityNotSet(string message) : base(message)
        {
        }
    }
}
