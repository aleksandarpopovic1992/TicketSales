using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSales.Shared.Projection;

namespace TicketSales.Admin.Projection.Concert
{
	public class ConcertProjection : IProjection
	{
		public Guid Id { get; }

		public string Title { get; }

        public int TicketBought { get; }

        public int MaximumNumberOfTickets { get; }

		public ConcertProjection(Guid id, string title, int tickets, int maximumNumberOfTickets)
		{
			Id = id;
			Title = title;
            TicketBought = tickets;
            MaximumNumberOfTickets = maximumNumberOfTickets;
		}

	}
}
