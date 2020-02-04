using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSales.Shared.Document;

namespace TicketSales.User.Projection.Concert
{
	public interface IConcertDocument: IDocument<ConcertProjection> 
	{
		bool TryGetConcert(Guid id, out ConcertProjection concertProjection);

		IEnumerable<ConcertProjection> GetConcerts();
	}
}
