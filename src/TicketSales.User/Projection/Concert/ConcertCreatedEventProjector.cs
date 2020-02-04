using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSales.Messages.Events;
using TicketSales.Shared.Projection;

namespace TicketSales.User.Projection.Concert
{
	public class ConcertCreatedEventProjector : IProjector<ConcertCreatedEvent>
	{
		private IConcertDocument _concerts;

		public ConcertCreatedEventProjector(IConcertDocument concerts)
		{
			_concerts = concerts;
		}

		public void Project(ConcertCreatedEvent @event)
		{
			ConcertProjection concertProjection = new ConcertProjection(@event.ConcertId,@event.Title);
			_concerts.Add(concertProjection);
		}
	}
}
