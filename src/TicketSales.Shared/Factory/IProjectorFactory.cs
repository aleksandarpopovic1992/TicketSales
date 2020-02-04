using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSales.Shared;
using TicketSales.Shared.Projection;

namespace TicketSales.Shared.Factory
{
	public interface IProjectorFactory<T> where T : IEvent
	{
		IProjector<T> CreateProjector();
	}
}
