using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSales.Messages.Events;
using TicketSales.Shared.Projection;
using TicketSales.User.Projection.Concert;

namespace TicketSales.User.Projection.Ticket
{
	public class TicketBoughtEventProjector : IProjector<ConcertTicketsBoughtEvent>
	{
		private ITicketDocument _tickets;
        private IConcertDocument _concerts;

		public TicketBoughtEventProjector(ITicketDocument tickets, IConcertDocument concerts)
		{
			_tickets = tickets;
            _concerts = concerts;
		}

		public void Project(ConcertTicketsBoughtEvent @event)
		{
            _concerts.TryGetConcert(@event.ConcertId,out ConcertProjection concert);

			TicketsBoughtProjection ticketBoughtProjection = new TicketsBoughtProjection(@event.UserId,@event.ConcertId,@event.NumberOfTickets,concert?.Title??string.Empty);

			_tickets.Add(ticketBoughtProjection);
		}
	}
}
