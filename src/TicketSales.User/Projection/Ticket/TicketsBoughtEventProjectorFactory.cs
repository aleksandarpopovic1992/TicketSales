using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSales.Messages.Events;
using TicketSales.Shared.Factory;
using TicketSales.Shared.Projection;
using TicketSales.User.Projection.Concert;


namespace TicketSales.User.Projection.Ticket
{
	public class TicketsBoughtEventProjectorFactory : IProjectorFactory<ConcertTicketsBoughtEvent>
	{
		private ITicketDocument _ticket;
        private IConcertDocument _concerts;

		public TicketsBoughtEventProjectorFactory(ITicketDocument tickets, IConcertDocument concerts)
		{
			 _ticket = tickets;
            _concerts = concerts;
		}

		public IProjector<ConcertTicketsBoughtEvent> CreateProjector()
		{
			return new TicketBoughtEventProjector(_ticket,_concerts);
		}
	}
}
