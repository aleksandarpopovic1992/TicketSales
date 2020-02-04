using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSales.Admin.Projection.Concert;
using TicketSales.Messages.Events;
using TicketSales.Shared.Projection;


namespace TicketSales.Admin.Projection.Ticket
{
	public class TicketBoughtEventProjector : IProjector<ConcertTicketsBoughtEvent>
	{
        private IConcertDocument _concerts;

		public TicketBoughtEventProjector(IConcertDocument concerts)
        { 
            _concerts = concerts;
		}

		public void Project(ConcertTicketsBoughtEvent @event)
		{
            if (!_concerts.TryGetConcert(@event.ConcertId, out ConcertProjection concert))
            {
                return;
            }

            _concerts.Update(new ConcertProjection(concert.Id,concert.Title,concert.TicketBought+@event.NumberOfTickets,concert.MaximumNumberOfTickets));
            
		}
	}
}
