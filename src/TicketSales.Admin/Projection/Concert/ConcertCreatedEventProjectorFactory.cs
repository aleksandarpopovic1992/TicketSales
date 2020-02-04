using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSales.Messages.Events;
using TicketSales.Shared.Factory;
using TicketSales.Shared.Projection;


namespace TicketSales.Admin.Projection.Concert
{
	public class ConcertCreatedEventProjectorFactory : IProjectorFactory<ConcertCreatedEvent>
	{
		private IConcertDocument _concerts;

        public ConcertCreatedEventProjectorFactory(IConcertDocument concerts)
        {
            _concerts = concerts;
        }

		public IProjector<ConcertCreatedEvent> CreateProjector()
		{
			return   new ConcertCreatedEventProjector(_concerts);
		}
	}
}
