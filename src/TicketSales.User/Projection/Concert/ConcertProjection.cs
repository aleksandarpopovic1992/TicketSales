using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSales.Shared.Projection;

namespace TicketSales.User.Projection.Concert
{
	public class ConcertProjection : IProjection
	{
		public Guid Id { get; }

		public string Title { get; }

		public ConcertProjection(Guid id, string title)
		{
			Id = id;
			Title = title;
		}

	}
}
