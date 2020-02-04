using System;
using System.Collections.Generic;
using System.Text;

namespace TicketSales.Messages.Commands
{
    public class BuyConcertTicketsCommand
    {
        public int NumberOfTicketsToBuy { get; } 

        public long UserId { get; }

        public Guid ConcertId { get; }

        public BuyConcertTicketsCommand(int numberOfTicketsToBuy, long userId, Guid concertId)
        {
            NumberOfTicketsToBuy = numberOfTicketsToBuy;

            UserId = userId;

            ConcertId = concertId;

        }

    }
}
