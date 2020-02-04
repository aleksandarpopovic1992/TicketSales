using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSales.Shared.Document;

namespace TicketSales.User.Projection.Ticket
{
	public interface ITicketDocument : IDocument<TicketsBoughtProjection>
	{
		IEnumerable<TicketsBoughtProjection> GetBy(long userId);
	}
}
