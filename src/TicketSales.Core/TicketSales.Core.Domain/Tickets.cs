using System;
using System.Collections.Generic;
using System.Text;

namespace TicketSales.Core.Domain
{
    public sealed class Tickets : ValueObject<Tickets>
    {
        public int Quantity { get; }


        public Tickets(int quantity)
        {
            Quantity = quantity;
        }

        public bool HasMoreThan(Tickets tickets)
        {
            return Quantity > tickets.Quantity;
        }

        public Tickets AddNumberOf(Tickets tickets)
        {
            return new Tickets(Quantity+tickets.Quantity);
        }

 
        protected override bool EqualsCore(Tickets other)
        {
            return other.Quantity == Quantity;
        }

        protected override int GetHashCodeCore()
        {
            return Quantity.GetHashCode();
        }


    }
}
