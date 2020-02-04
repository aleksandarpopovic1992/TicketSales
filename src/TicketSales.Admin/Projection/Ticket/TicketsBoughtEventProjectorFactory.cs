using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSales.Admin.Projection.Concert;
using TicketSales.Messages.Events;
using TicketSales.Shared.Factory;
using TicketSales.Shared.Projection;


namespace TicketSales.Admin.Projection.Ticket
{
	public class TicketsBoughtEventProjectorFactory : IProjectorFactory<ConcertTicketsBoughtEvent>
	{
        private IConcertDocument _concerts;

		public TicketsBoughtEventProjectorFactory(IConcertDocument concerts)
		{
            _concerts = concerts;
		}

		public IProjector<ConcertTicketsBoughtEvent> CreateProjector()
		{
			return new TicketBoughtEventProjector(_concerts);
		}
	}
}
