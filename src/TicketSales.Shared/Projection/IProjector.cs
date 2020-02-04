using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TicketSales.Shared.Projection
{
	public interface IProjector<T> where T : IEvent
	{
		void Project(T @event);
	}
}
