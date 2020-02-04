using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSales.Shared.Projection;

namespace TicketSales.User.Projection.Ticket
{
	public class TicketsBoughtProjection : IProjection
	{
		public long UserId { get;}

		public Guid ConcertId { get; }

		public int TicketsBought { get; }

        public string Title { get; }

		public TicketsBoughtProjection(long userId, Guid concertId, int ticketsBought,string title)
		{
			UserId = userId;
			ConcertId = concertId;
			TicketsBought = ticketsBought;
            Title = title;
		}

	}
}
