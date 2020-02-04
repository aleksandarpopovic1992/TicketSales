using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSales.Messages.Events;
using TicketSales.Shared.Projection;

namespace TicketSales.Admin.Projection.Concert
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
			ConcertProjection concertProjection = new ConcertProjection(@event.ConcertId,@event.Title,0,@event.NumberOfTickets);
			_concerts.Add(concertProjection);
		}
	}
}
