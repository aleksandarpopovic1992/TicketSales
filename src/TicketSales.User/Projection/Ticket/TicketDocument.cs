using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSales.User.Projection.Ticket
{
	public class TicketDocument : ITicketDocument
	{
		private IDictionary<long, List<TicketsBoughtProjection>> _tickets;
        private static readonly object _object = new object();

        public TicketDocument()
		{
			_tickets = new Dictionary<long, List<TicketsBoughtProjection>>();
		}

		public void Add(TicketsBoughtProjection item)
		{
            lock(_object) {
                List<TicketsBoughtProjection> tickets;
                if (!_tickets.TryGetValue(item.UserId, out tickets))
                {
                    tickets = new List<TicketsBoughtProjection>();
                    _tickets[item.UserId] = tickets;
                }

                tickets.Add(item);
            }

		}

		public IEnumerable<TicketsBoughtProjection> GetBy(long userId)
		{
			List<TicketsBoughtProjection> tickets;
			if (!_tickets.TryGetValue(userId, out tickets))
			{
				return new List<TicketsBoughtProjection>();
			}

			return tickets;
		}

	}
}
