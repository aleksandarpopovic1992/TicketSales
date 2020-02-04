using System;
using System.Collections.Generic;
using System.Text;

namespace TicketSales.Core.Domain.Events
{
   public class ConcertTicketsBoughtDomainEvent : DomainEvent
   {
        public int NumberOfTicketsToBuy { get; }

        public long UserId { get; }

        public Guid ConcertId { get; }

        public ConcertTicketsBoughtDomainEvent(int numberOfTicketsToBuy, long userId, Guid concertId)
        {
            NumberOfTicketsToBuy = numberOfTicketsToBuy;

            UserId = userId;

            ConcertId = concertId;
        }
    }
}
